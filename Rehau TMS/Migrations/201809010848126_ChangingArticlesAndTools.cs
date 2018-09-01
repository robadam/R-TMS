namespace Rehau_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingArticlesAndTools : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "ArticleSerialNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Tools", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Tools", "SerialNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tools", "SerialNumber", c => c.String());
            AlterColumn("dbo.Tools", "Name", c => c.String());
            AlterColumn("dbo.Articles", "ArticleSerialNumber", c => c.String());
            AlterColumn("dbo.Articles", "Name", c => c.String());
        }
    }
}
