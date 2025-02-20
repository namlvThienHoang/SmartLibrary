namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationType", c => c.String());
            AddColumn("dbo.Notifications", "RedirectUrl", c => c.String());
            AddColumn("dbo.Notifications", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Title");
            DropColumn("dbo.Notifications", "RedirectUrl");
            DropColumn("dbo.Notifications", "NotificationType");
        }
    }
}
