namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotification1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            CreateTable(
                "dbo.NotificationUsers",
                c => new
                    {
                        NotificationId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.UserId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Notifications", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.NotificationUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationUsers", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.NotificationUsers", new[] { "UserId" });
            DropIndex("dbo.NotificationUsers", new[] { "NotificationId" });
            DropTable("dbo.NotificationUsers");
            CreateIndex("dbo.Notifications", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
