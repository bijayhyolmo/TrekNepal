using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekNepal.Models;

namespace TrekNepal.ViewModels
{
    public class PackageShortInfoViewModel
    {
        public int Id { get; set; }

        public string PackageType { get; set; }

        public string PackageTitle { get; set; }

        public string DurationInWord { get; set; }

        public string Price { get; set; }

        public string FeaturedImageLink { get; set; }

        public static List<System.Linq.IGrouping<string, PackageShortInfoViewModel>> GetPackagesInfo()
        {
            return GetPackages().GroupBy(x => x.PackageType).ToList();
        }

        public static List<PackageShortInfoViewModel> GetPackages()
        {
            using (var context = new ApplicationDbContext())
            {
                var packages =  context.Packages.ToList().Select(p => new PackageShortInfoViewModel
                {
                    Id = p.Id,
                    PackageType = p.PackageType,
                    PackageTitle = p.PackageTitle,
                    DurationInWord = p.DurationInWord,
                    Price = string.Format("$ {0}", p.PackagePrice),
                    FeaturedImageLink = p.FeaturedImage,
                }).ToList();
                return packages;
            }
        }

        public static PackageDetailViewModel GetPackageDetails(int id)
        {
            return new PackageDetailViewModel();
        }
    }

    public class PackageDetailViewModel
    {
        public ProductHightLight ProductHighLight { get; set; }
    }

    public class ProductHightLight
    {
        public string Price { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Duration { get; set; }
        public string Difficulty { get; set; }
        public string MaxElevation { get; set; }
        public List<Offer> Offers { get; set; }
    }

    public class Offer
    {
        public string OfferTitle { get; set; }
        public string OfferValue { get; set; }
    }
}