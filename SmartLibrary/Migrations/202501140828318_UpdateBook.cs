namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuditLogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AuditLogs", new[] { "UserId" });
            AlterColumn("dbo.AuditLogs", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AuditLogs", "UserId");
            AddForeignKey("dbo.AuditLogs", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditLogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AuditLogs", new[] { "UserId" });
            AlterColumn("dbo.AuditLogs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AuditLogs", "UserId");
            AddForeignKey("dbo.AuditLogs", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
