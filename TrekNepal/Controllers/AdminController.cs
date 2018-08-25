using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrekNepal.Models;

namespace TrekNepal.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private Dictionary<int, TrekRoute> _stop;
        public AdminController()
        {
            _context = ApplicationDbContext.Create();
        }

        public async Task<ActionResult> Dashboard()
        {
            return View();
        }

        public async Task<ActionResult> Packages()
        {
            var packages = _context.Packages.ToList();
            return View(packages);
        }

        public async Task<ActionResult> SavePackage(int id)
        {
            TempData["TempRoutes"] = null;
            TempData["selectedIndex"] = null;
            if (id > 0)
            {
                var package = await _context.Packages.FindAsync(id);
                if (package.RouteDetails.Any())
                {
                    var routes = new Dictionary<int, TrekRoute>();
                    for (var i = 0; i < package.RouteDetails.Count; i++)
                    {
                        routes.Add(i, package.RouteDetails[i]);
                    }
                    TempData["TempRoutes"] = routes;
                }
                return PartialView(package);
            }
            else
            {
                return PartialView(new TrekPackage());
            }
        }

        [HttpPost]
        public async Task<ActionResult> SavePackage(TrekPackage trekPackage, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                var filename = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/savedImages"), filename);
                image.SaveAs(path);
                trekPackage.FeaturedImage = "/Content/savedImages/" + filename;
            }
            if (trekPackage.Id > 0)
            {
                var tempData = Session[0] as Dictionary<int, TrekRoute>;
                await _context.Routes.Where(x => x.TrekPackageId == trekPackage.Id).ForEachAsync((x) =>
                {
                    _context.Entry(x).State = EntityState.Deleted;
                    _context.SaveChanges();
                });
                trekPackage.RouteDetails = tempData.Select(x => new TrekRoute
                {
                    Id = 0,
                    From = x.Value.From,
                    To = x.Value.To,
                    Elevation = x.Value.Elevation,
                    Activities = x.Value.Activities,
                    Order = x.Value.Order,
                    RouteDescription = x.Value.RouteDescription,
                    TrekPackageId = x.Value.TrekPackageId,
                }).ToList();
                using (var context = ApplicationDbContext.Create())
                {
                    context.Entry(trekPackage).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                TempData["TempRoutes"] = null;
                TempData["selectedIndex"] = null;
                Session.Clear();
                return RedirectToAction("Packages");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var tempData = Session[0] as Dictionary<int, TrekRoute>;
                    trekPackage.RouteDetails = tempData.Select(x => new TrekRoute
                    {
                        Id = x.Value.Id,
                        From = x.Value.From,
                        To = x.Value.To,
                        Elevation = x.Value.Elevation,
                        Activities = x.Value.Activities,
                        Order = x.Value.Order,
                        RouteDescription = x.Value.RouteDescription,
                        TrekPackageId = x.Value.TrekPackageId,
                    }).ToList();
                    _context.Packages.Add(trekPackage);
                    await _context.SaveChangesAsync();
                    TempData["TempRoutes"] = null;
                    TempData["selectedIndex"] = null;
                    Session.Clear();
                    return RedirectToAction("Packages");
                }
                else
                {
                    throw new Exception();
                }
            }
        }


        public async Task<ActionResult> SaveRoute(int index, int packageId)
        {
            var tempData = TempData["TempRoutes"] as Dictionary<int, TrekRoute>;
            if (Session.Count > 0)
            {
                var temp = Session[0] as Dictionary<int, TrekRoute>;
                if (temp != null)
                {
                    tempData = temp;
                }
            }
            if (tempData == null)
            {
                tempData = new Dictionary<int, TrekRoute>();
            }
            var route = new TrekRoute();
            if (tempData.Any())
            {
                route.TrekPackageId = packageId;
                if (tempData.ContainsKey(index) && index >= 0)
                {
                    TempData["selectedIndex"] = index;
                    route = tempData.First(x => x.Key == index).Value;
                }
                else
                {
                    TempData["selectedIndex"] = tempData.Count;
                    tempData.Add(tempData.Count, route);
                    TempData["TempRoutes"] = tempData;
                }
            }
            else
            {
                TempData["selectedIndex"] = tempData.Count;
                route.TrekPackageId = packageId;
                tempData.Add(tempData.Count, route);
                TempData["TempRoutes"] = tempData;
            }
            return PartialView(route);
        }

        [HttpPost]
        public async Task<ActionResult> SaveRoute(TrekRoute trekRoute)
        {
            var tempData = TempData["TempRoutes"] as Dictionary<int, TrekRoute>;
            if (TempData["selectedIndex"] != null && ((int)TempData["selectedIndex"]) > -1)
            {
                int index = (int)TempData["selectedIndex"];
                tempData[index] = trekRoute;
                TempData["selectedIndex"] = null;
            }
            TempData["TempRoutes"] = tempData;
            return PartialView();
        }

        public async Task<ActionResult> Routes()
        {
            var tempData = TempData["TempRoutes"] as Dictionary<int, TrekRoute>;
            TempData["TeamRoutes"] = tempData;
            if (Session.Keys.Count > 0)
            {
                Session[0] = tempData;
            }
            else
            {
                Session.Add("val", tempData);
            }
            return PartialView(tempData);
        }

        public async Task<ActionResult> Offers()
        {
            var offers = _context.Offers.ToList();
            return View(offers);
        }

        public async Task<ActionResult> SaveOffer(int id)
        {
            if (id > 0)
            {
                var offer = await _context.Offers.FindAsync(id);
                return PartialView(offer);
            }
            else
            {
                return PartialView(new Offer());
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveOffer(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Offers");
            }

            return PartialView(offer);
        }

        public async Task<ActionResult> Bookings()
        {
            var bookings = _context.Bookings.OrderByDescending(x => x.BookFor);
            return View(bookings);
        }

        public async Task<ActionResult> GetBookingDetail(int id)
        {
            if (id > 0)
            {
                return PartialView(await _context.Bookings.FindAsync(id));
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public async Task<bool> MarkBookingAsCleared(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            booking.Cleared = true;
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        [HttpGet]
        public async Task<ActionResult> Settings()
        {
            var setting = _context.Settings.First();
            return View(setting);
        }

        [HttpPost]
        public async Task<ActionResult> SaveSettings(Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(setting).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return PartialView(setting);
        }

        public async Task<ActionResult> AboutUs()
        {
            var aboutus = _context.AboutUsContents.OrderBy(x => x.Order).ToList();
            return View(aboutus);
        }

        public async Task<ActionResult> SaveAboutUs(int id)
        {
            if (id > 0)
            {
                return PartialView(await _context.AboutUsContents.FindAsync(id));
            }
            else
            {
                var count = await _context.AboutUsContents.CountAsync();
                var content = new AboutUsContent();
                content.Heading = string.Empty;
                content.Content = string.Empty;
                content.Order = count + 1;
                return PartialView(content);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveAboutUs(AboutUsContent aboutUsContent)
        {
            if (aboutUsContent.Id > 0)
            {
                var currentContent = _context.AboutUsContents.Find(aboutUsContent.Id);
                if (currentContent.Order > aboutUsContent.Order)
                {
                    foreach (var temp in _context.AboutUsContents.Where(x => x.Order >= aboutUsContent.Order && x.Order < currentContent.Order && x.Id != aboutUsContent.Id).ToList())
                    {
                        temp.Order += 1;
                        _context.Entry(temp).State = EntityState.Modified;
                    }
                }
                else if (currentContent.Order < aboutUsContent.Order)
                {
                    foreach (var temp in _context.AboutUsContents.Where(x => x.Order <= aboutUsContent.Order && x.Order > currentContent.Order && x.Id != aboutUsContent.Id).ToList())
                    {
                        temp.Order -= 1;
                        _context.Entry(temp).State = EntityState.Modified;
                    }
                }
                currentContent.Content = aboutUsContent.Content;
                currentContent.Heading = aboutUsContent.Heading;
                currentContent.Order = aboutUsContent.Order;
                _context.Entry(currentContent).State = EntityState.Modified;
            }
            else
            {
                _context.AboutUsContents.Add(aboutUsContent);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("AboutUs");
        }

        public async Task<ActionResult> DelAboutUs(int id)
        {
            if (id > 0)
            {
                var currentContent = _context.AboutUsContents.Find(id);
                _context.Entry(currentContent).State = EntityState.Deleted;
                foreach (var temp in _context.AboutUsContents.Where(x => x.Order > currentContent.Order))
                {
                    temp.Order -= 1;
                    _context.Entry(temp).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("AboutUs");
        }

        public async Task<ActionResult> Queries()
        {
            return View(_context.Queries.ToList());
        }
    }
}