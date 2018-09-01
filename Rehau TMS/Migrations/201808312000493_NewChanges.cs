namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        WorkTypeId = c.Int(nullable: false),
                        ToolId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        OptionsId = c.Int(nullable: false),
                        OptionsAdditionalId = c.Int(nullable: false),
                        ReportedHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Options", t => t.OptionsId, cascadeDelete: true)
                .ForeignKey("dbo.OptionsAdditionals", t => t.OptionsAdditionalId, cascadeDelete: true)
                .ForeignKey("dbo.Tools", t => t.ToolId, cascadeDelete: true)
                .ForeignKey("dbo.WorkTypes", t => t.WorkTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.WorkTypeId)
                .Index(t => t.ToolId)
                .Index(t => t.ArticleId)
                .Index(t => t.OptionsId)
                .Index(t => t.OptionsAdditionalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "WorkTypeId", "dbo.WorkTypes");
            DropForeignKey("dbo.Schedules", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals");
            DropForeignKey("dbo.Schedules", "OptionsId", "dbo.Options");
            DropForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "OptionsAdditionalId" });
            DropIndex("dbo.Schedules", new[] { "OptionsId" });
            DropIndex("dbo.Schedules", new[] { "ArticleId" });
            DropIndex("dbo.Schedules", new[] { "ToolId" });
            DropIndex("dbo.Schedules", new[] { "WorkTypeId" });
            DropIndex("dbo.Schedules", new[] { "ApplicationUserId" });
            DropTable("dbo.Schedules");
        }
    }
}
