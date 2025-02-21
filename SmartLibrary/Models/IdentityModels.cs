using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models.EntityModels;

namespace SmartLibrary.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string AvatarURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual ICollection<UserSetting> UserSettings { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<NotificationUser> NotificationUsers { get; set; }
        public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // DbSet declarations
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }
        public DbSet<FineTransaction> FineTransactions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<LibrarySetting> LibrarySettings { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Identity configurations
            base.OnModelCreating(modelBuilder);

            // BookAuthor configuration
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasRequired(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasRequired(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // BookCategory configuration
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasRequired(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasRequired(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // BorrowTransaction configuration
            modelBuilder.Entity<BorrowTransaction>()
                .HasRequired(bt => bt.Book)
                .WithMany()
                .HasForeignKey(bt => bt.BookId);

            modelBuilder.Entity<BorrowTransaction>()
                .HasRequired(bt => bt.User)
                .WithMany()
                .HasForeignKey(bt => bt.UserId);

            // FineTransaction configuration
            modelBuilder.Entity<FineTransaction>()
                .HasRequired(ft => ft.BorrowTransaction)
                .WithMany()
                .HasForeignKey(ft => ft.BorrowTransactionId);

            // BookReview configuration
            modelBuilder.Entity<BookReview>()
                .HasRequired(br => br.Book)
                .WithMany(b => b.BookReviews)
                .HasForeignKey(br => br.BookId);

            modelBuilder.Entity<BookReview>()
                .HasRequired(br => br.User)
                .WithMany()
                .HasForeignKey(br => br.UserId);

            // Reservation configuration
            modelBuilder.Entity<Reservation>()
                .HasRequired(r => r.Book)
                .WithMany()
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Reservation>()
                .HasRequired(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // Membership configuration
            modelBuilder.Entity<Membership>()
                .HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            // BookAuthor configuration
            modelBuilder.Entity<NotificationUser>()
                .HasKey(ba => new { ba.NotificationId, ba.UserId });

            modelBuilder.Entity<NotificationUser>()
                .HasRequired(ba => ba.Notification)
                .WithMany(b => b.NotificationUsers)
                .HasForeignKey(ba => ba.NotificationId);

            modelBuilder.Entity<NotificationUser>()
                .HasRequired(ba => ba.User)
                .WithMany(a => a.NotificationUsers)
                .HasForeignKey(ba => ba.UserId);


            modelBuilder.Entity<UserSetting>()
            .HasRequired(us => us.User)
            .WithMany(u => u.UserSettings)
            .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<AuditLog>()
            .HasRequired(a => a.User)
            .WithMany() // Adjust as necessary if the user has many audit logs
            .HasForeignKey(a => a.UserId);

            // LibrarySetting, AuditLog do not require special relations
        }
    }
}