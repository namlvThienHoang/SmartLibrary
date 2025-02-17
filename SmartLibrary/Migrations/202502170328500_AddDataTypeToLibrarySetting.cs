namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataTypeToLibrarySetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LibrarySettings", "DataType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LibrarySettings", "DataType");
        }
    }
}
