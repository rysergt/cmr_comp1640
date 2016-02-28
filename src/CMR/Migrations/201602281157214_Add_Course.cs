namespace CMR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Course : DbMigration
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
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "User_Id" });
            DropTable("dbo.Courses");
        }
    }
}
