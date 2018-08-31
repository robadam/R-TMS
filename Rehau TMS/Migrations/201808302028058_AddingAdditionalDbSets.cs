namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAdditionalDbSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArticleSerialNumber = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleId);
            
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
                "dbo.Tools",
                c => new
                    {
                        ToolId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ToolsStatusId = c.Int(nullable: false),
                        ArticlesId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.ToolId);
            
            CreateTable(
                "dbo.ToolStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkTypes");
            DropTable("dbo.ToolStatus");
            DropTable("dbo.Tools");
            DropTable("dbo.OptionsAdditionals");
            DropTable("dbo.Options");
            DropTable("dbo.Articles");
        }
    }
}
