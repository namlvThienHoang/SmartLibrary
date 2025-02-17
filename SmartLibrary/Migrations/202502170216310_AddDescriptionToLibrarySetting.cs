namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToLibrarySetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LibrarySettings", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LibrarySettings", "Description");
        }
    }
}
