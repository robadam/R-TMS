namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScheduleModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArticleSerialNumber = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ToolsModelStateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionsAdditionals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OptionsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ReportedHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Tools", t => t.ToolId, cascadeDelete: true)
                .ForeignKey("dbo.WorkTypes", t => t.WorkTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.WorkTypeId)
                .Index(t => t.ToolId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ToolsStatusId = c.Int(nullable: false),
                        ArticlesId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ToolStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "WorkTypeId", "dbo.WorkTypes");
            DropForeignKey("dbo.Schedules", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Schedules", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "ArticleId" });
            DropIndex("dbo.Schedules", new[] { "ToolId" });
            DropIndex("dbo.Schedules", new[] { "WorkTypeId" });
            DropIndex("dbo.Schedules", new[] { "ApplicationUserId" });
            DropTable("dbo.ToolStatus");
            DropTable("dbo.WorkTypes");
            DropTable("dbo.Tools");
            DropTable("dbo.Schedules");
            DropTable("dbo.OptionsAdditionals");
            DropTable("dbo.Options");
            DropTable("dbo.Articles");
        }
    }
}
