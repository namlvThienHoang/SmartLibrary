namespace SmartLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Entity = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        Details = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AuditLogId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BorrowTransactions",
                c => new
                    {
                        BorrowTransactionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        Status = c.String(),
                        Book_BookId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BorrowTransactionId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.Book_BookId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Publisher = c.String(),
                        PublishedDate = c.DateTime(nullable: false),
                        ISBN = c.String(),
                        TotalCopies = c.Int(nullable: false),
                        AvailableCopies = c.Int(nullable: false),
                        CoverImage = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.AuthorId })
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.CategoryId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.BookReviews",
                c => new
                    {
                        BookReviewId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                        Review = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookReviewId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                        CancelDate = c.DateTime(),
                        Book_BookId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.Book_BookId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.FineTransactions",
                c => new
                    {
                        FineTransactionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BorrowTransactionId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FineDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FineTransactionId)
                .ForeignKey("dbo.BorrowTransactions", t => t.BorrowTransactionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BorrowTransactionId);
            
            CreateTable(
                "dbo.LibrarySettings",
                c => new
                    {
                        LibrarySettingId = c.Int(nullable: false, identity: true),
                        SettingKey = c.String(),
                        SettingValue = c.String(),
                    })
                .PrimaryKey(t => t.LibrarySettingId);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Message = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FineTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FineTransactions", "BorrowTransactionId", "dbo.BorrowTransactions");
            DropForeignKey("dbo.AuditLogs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowTransactions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowTransactions", "BookId", "dbo.Books");
            DropForeignKey("dbo.Reservations", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "BookId", "dbo.Books");
            DropForeignKey("dbo.BorrowTransactions", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookReviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BookCategories", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropIndex("dbo.FineTransactions", new[] { "BorrowTransactionId" });
            DropIndex("dbo.FineTransactions", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reservations", new[] { "Book_BookId" });
            DropIndex("dbo.Reservations", new[] { "BookId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.BookReviews", new[] { "UserId" });
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            DropIndex("dbo.BookCategories", new[] { "CategoryId" });
            DropIndex("dbo.BookCategories", new[] { "BookId" });
            DropIndex("dbo.BookAuthors", new[] { "AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "BookId" });
            DropIndex("dbo.BorrowTransactions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.BorrowTransactions", new[] { "Book_BookId" });
            DropIndex("dbo.BorrowTransactions", new[] { "BookId" });
            DropIndex("dbo.BorrowTransactions", new[] { "UserId" });
            DropIndex("dbo.AuditLogs", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Status");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropTable("dbo.Notifications");
            DropTable("dbo.Memberships");
            DropTable("dbo.LibrarySettings");
            DropTable("dbo.FineTransactions");
            DropTable("dbo.Reservations");
            DropTable("dbo.BookReviews");
            DropTable("dbo.Categories");
            DropTable("dbo.BookCategories");
            DropTable("dbo.Authors");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Books");
            DropTable("dbo.BorrowTransactions");
            DropTable("dbo.AuditLogs");
        }
    }
}
