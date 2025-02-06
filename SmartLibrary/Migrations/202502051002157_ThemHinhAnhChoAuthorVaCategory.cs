namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemHinhAnhChoAuthorVaCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "AvatarImage", c => c.String());
            AddColumn("dbo.Categories", "CoverImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CoverImage");
            DropColumn("dbo.Authors", "AvatarImage");
        }
    }
}
