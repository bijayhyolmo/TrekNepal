namespace TrekNepal.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TrekNepal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TrekNepal.Models.ApplicationDbContext";
        }

        protected override void Seed(TrekNepal.Models.ApplicationDbContext context)
        {
            if (!context.Settings.Any())
            {
                context.Settings.Add(new Models.Setting
                {
                    Id = 0,
                    Currency = "Rs",
                    FbLink = "http://fb.com",
                    InstaLink = "http://insta.com",
                    TwitterLink = "http://twitter.com",
                    LandlineNumber = "01-44-000000",
                    MobileNumber = "9841-000000",
                    ViberNumber = "9841-000000",
                    Location = "Helambu",
                    DetailedLocation = "Yambalama, Near Thulo Dhunga"
                });
            }
            if (!context.Users.Any())
            {
                context.Users.Add(new Models.ApplicationUser()
                {
                    Email="admin@admin.com",
                    UserName="admin",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PasswordHash = new PasswordHasher().HashPassword("admin123"),
                    AccessFailedCount = 0,
                    PhoneNumber = "00-00-000000",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled=false,
                });
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
