namespace SmartLibrary.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SmartLibrary.Models;
    using SmartLibrary.Models.EntityModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartLibrary.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartLibrary.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Create RoleManager and UserManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Define roles you want to create
            string[] roleNames = { "Admin", "User", "Manager" };

            // Create each role if it doesn't exist
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    roleManager.Create(new IdentityRole(roleName));
                }
            }

            // Optionally: Create a default user and assign a role
            var user = userManager.FindByEmail("admin@smartlibrary.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@smartlibrary.com",
                    Email = "admin@smartlibrary.com",
                    // Add other user properties as needed
                };

                var result = userManager.Create(user, "Admin@123"); // Specify a password
                if (result.Succeeded)
                {
                    // Assign the "Admin" role to the user
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            base.Seed(context); // Ensure the base seed method is called

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { CategoryName = "Fiction", Description = "Fictional books" },
                    new Category { CategoryName = "Science", Description = "Science-related books" },
                    new Category { CategoryName = "History", Description = "Historical books" }
                };
                context.Categories.AddRange(categories);
            }


            // Seed Authors
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                   new Author
                    {
                        AuthorName = "F. Scott Fitzgerald",
                        DateOfBirth = new DateTime(1896, 9, 24),
                        Biography = "F. Scott Fitzgerald was an American novelist and short-story writer."
                    },
                    new Author
                    {
                        AuthorName = "Stephen Hawking",
                        DateOfBirth = new DateTime(1942, 1, 8),
                        Biography = "Stephen Hawking was a theoretical physicist and cosmologist."
                    },
                    new Author
                    {
                        AuthorName = "Yuval Noah Harari",
                        DateOfBirth = new DateTime(1976, 2, 24),
                        Biography = "Yuval Noah Harari is an Israeli historian and author."
                    }
                };
                context.Authors.AddRange(authors);
            }

            // Seed Books
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Title = "The Great Gatsby", ISBN = "9781234567890", PublishedDate = new DateTime(1925, 9, 24) },
                    new Book { Title = "A Brief History of Time", ISBN = "9789876543210", PublishedDate = new DateTime(1988, 9, 24) },
                    new Book { Title = "Sapiens: A Brief History of Humankind", ISBN = "9781594205224", PublishedDate = new DateTime(2011, 9, 24) }
                };
                context.Books.AddRange(books);
            }

            // Seed Library Settings
            if (!context.LibrarySettings.Any())
            {
                var settings = new List<LibrarySetting>
                {
                    new LibrarySetting { SettingKey = "MaxBorrowDays", SettingValue = "14" },
                    new LibrarySetting { SettingKey = "LateFeePerDay", SettingValue = "1.50" }
                };
                context.LibrarySettings.AddRange(settings);
            }

            // Save changes
            context.SaveChanges();
        }
    }
}
