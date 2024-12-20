namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "AuthorName", c => c.String());
            DropColumn("dbo.Authors", "FirstName");
            DropColumn("dbo.Authors", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "LastName", c => c.String());
            AddColumn("dbo.Authors", "FirstName", c => c.String());
            DropColumn("dbo.Authors", "AuthorName");
        }
    }
}
