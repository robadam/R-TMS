namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Schedules", "OptionsId", "dbo.Options");
            DropForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals");
            DropForeignKey("dbo.Schedules", "ToolId", "dbo.Tools");
            DropIndex("dbo.Schedules", new[] { "ToolId" });
            DropIndex("dbo.Schedules", new[] { "ArticleId" });
            DropIndex("dbo.Schedules", new[] { "OptionsId" });
            DropIndex("dbo.Schedules", new[] { "OptionsAdditionalId" });
            AlterColumn("dbo.Schedules", "ToolId", c => c.Int());
            AlterColumn("dbo.Schedules", "ArticleId", c => c.Int());
            AlterColumn("dbo.Schedules", "OptionsId", c => c.Int());
            AlterColumn("dbo.Schedules", "OptionsAdditionalId", c => c.Int());
            CreateIndex("dbo.Schedules", "ToolId");
            CreateIndex("dbo.Schedules", "ArticleId");
            CreateIndex("dbo.Schedules", "OptionsId");
            CreateIndex("dbo.Schedules", "OptionsAdditionalId");
            AddForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles", "Id");
            AddForeignKey("dbo.Schedules", "OptionsId", "dbo.Options", "Id");
            AddForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals", "Id");
            AddForeignKey("dbo.Schedules", "ToolId", "dbo.Tools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals");
            DropForeignKey("dbo.Schedules", "OptionsId", "dbo.Options");
            DropForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Schedules", new[] { "OptionsAdditionalId" });
            DropIndex("dbo.Schedules", new[] { "OptionsId" });
            DropIndex("dbo.Schedules", new[] { "ArticleId" });
            DropIndex("dbo.Schedules", new[] { "ToolId" });
            AlterColumn("dbo.Schedules", "OptionsAdditionalId", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "OptionsId", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "ArticleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "ToolId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "OptionsAdditionalId");
            CreateIndex("dbo.Schedules", "OptionsId");
            CreateIndex("dbo.Schedules", "ArticleId");
            CreateIndex("dbo.Schedules", "ToolId");
            AddForeignKey("dbo.Schedules", "ToolId", "dbo.Tools", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "OptionsAdditionalId", "dbo.OptionsAdditionals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "OptionsId", "dbo.Options", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schedules", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
    }
}
