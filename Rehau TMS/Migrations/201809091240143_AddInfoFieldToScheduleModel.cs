namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInfoFieldToScheduleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "Info", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "Info");
        }
    }
}
