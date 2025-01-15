namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserIdTypeInUserSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Theme = c.String(),
                        Language = c.String(),
                        EmailNotifications = c.Boolean(nullable: false),
                        SmsNotifications = c.Boolean(nullable: false),
                        GridView = c.Boolean(nullable: false),
                        BooksPerPage = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Reservations", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSettings", new[] { "UserId" });
            DropColumn("dbo.Reservations", "Status");
            DropColumn("dbo.AspNetUsers", "UpdatedAt");
            DropColumn("dbo.AspNetUsers", "CreatedAt");
            DropTable("dbo.UserSettings");
        }
    }
}
