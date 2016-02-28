namespace CMR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Course_Creator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "Creator_Id");
            AddForeignKey("dbo.Courses", "Creator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Creator_Id" });
            DropColumn("dbo.Courses", "Creator_Id");
        }
    }
}
