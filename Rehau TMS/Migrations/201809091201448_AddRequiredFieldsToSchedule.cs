namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredFieldsToSchedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Schedules", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Schedules", "ApplicationUserId");
            AddForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Schedules", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Schedules", "ApplicationUserId");
            AddForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
