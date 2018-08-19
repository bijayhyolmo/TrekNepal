using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrekNepal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<TrekPackage> Packages { get; set; }
        public DbSet<TrekRoute> Routes { get; set; }
        public DbSet<PackageOffer> PackageOffers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RouteImage> RouteImages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<OnlineQuery> Queries { get; set; }
        public DbSet<AboutUsContent> AboutUsContents { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PackageOffer>().HasKey(x => new { x.OfferId, x.PackageId });
            modelBuilder.Entity<PackageOffer>().HasRequired(x => x.Package).WithMany(x => x.Offers).HasForeignKey(x => x.PackageId);
            modelBuilder.Entity<PackageOffer>().HasRequired(x => x.Offer).WithMany(x => x.Packages).HasForeignKey(x => x.OfferId);
        }
    }
}