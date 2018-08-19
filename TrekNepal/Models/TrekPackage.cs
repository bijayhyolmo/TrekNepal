using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrekNepal.Models
{
    public class TrekPackage
    {
        public int Id { get; set; }

        public string PackageTitle { get; set; }

        public string PackageType { get; set; }

        public int Night { get; set; }

        public int Day { get; set; }

        public string DurationInWord { get; set; }

        public string Difficulty { get; set; }

        public double PackagePrice { get; set; }

        public string FeaturedImage { get; set; }

        public virtual List<TrekRoute> RouteDetails { get; set; }

        public virtual List<PackageOffer> Offers { get; set; }

        public virtual List<Booking> Bookings { get; set; }

    }

    public class PackageOffer
    {
        public int OfferId { get; set; }

        public int PackageId { get; set; }

        public virtual TrekPackage Package { get; set; }

        public virtual Offer Offer { get; set; }
    }


    public class Offer
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Offers { get; set; }

        public string Description { get; set; }

        public virtual List<PackageOffer> Packages { get; set; }
    }

    public class TrekRoute
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string RouteDescription { get; set; }

        public int Elevation { get; set; }

        public string Activities { get; set; }

        public int TrekPackageId { get; set; }

        public virtual TrekPackage TrekPackage { get; set; }

        public virtual List<RouteImage> Images { get; set; }
    }

    public class RouteImage
    {
        public int Id { get; set; }

        public int RouteId { get; set; }

        public string ImageLink { get; set; }

        public int Order { get; set; }

        public string Captions { get; set; }

        public virtual TrekRoute Route { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }

        public string ImageLinks { get; set; }

        public bool Featured { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }

        public int PackageId { get; set; }

        public string BookedBy { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string AlternateNumber { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime BookedDate { get; set; }

        public Nullable<DateTime> BookFor { get; set; }

        public bool Cleared { get; set; }

        public virtual TrekPackage Package { get; set; }
    }

    public class Setting
    {
        public int Id { get; set; }

        public string Currency { get; set; }

        public string ViberNumber { get; set; }

        public string MobileNumber { get; set; }

        public string LandlineNumber { get; set; }

        public string FbLink { get; set; }

        public string TwitterLink { get; set; }

        public string InstaLink { get; set; }

        public string Location { get; set; }

        public string DetailedLocation { get; set; }

    }
}