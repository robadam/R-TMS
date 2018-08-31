namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendFieldsOfScheduleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "OptionsId", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "OptionsAdditionalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "OptionsId");
            CreateIndex("dbo.Schedules", "OptionsAdditionalId");
            AddForeignKey("dbo.Schedules", "OptionsId", "dbo.Options", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals");
            DropForeignKey("dbo.Schedules", "OptionsId", "dbo.Options");
            DropIndex("dbo.Schedules", new[] { "OptionsAdditionalId" });
            DropIndex("dbo.Schedules", new[] { "OptionsId" });
            DropColumn("dbo.Schedules", "OptionsAdditionalId");
            DropColumn("dbo.Schedules", "OptionsId");
        }
    }
}
