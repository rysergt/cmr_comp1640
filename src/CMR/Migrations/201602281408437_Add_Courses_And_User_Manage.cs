namespace CMR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Courses_And_User_Manage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignDate = c.DateTime(nullable: false),
                        Course_Id = c.Int(),
                        Manager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Manager_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Manager_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "Manager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Assignments", new[] { "Manager_Id" });
            DropIndex("dbo.Assignments", new[] { "Course_Id" });
            DropTable("dbo.Assignments");
            DropTable("dbo.Courses");
        }
    }
}
