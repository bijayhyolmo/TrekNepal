using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrekNepal.Models;
using TrekNepal.ViewModels;

namespace TrekNepal.Controllers
{
    public class PublicController : Controller
    {
        private ApplicationDbContext _context;
        public PublicController()
        {
            _context = ApplicationDbContext.Create();
        }
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PackageDetail(int packageid)
        {
            return PartialView();
        }

        public async Task<ActionResult> AboutUs()
        {
            var contents = _context.AboutUsContents.OrderBy(x => x.Order).ToList();
            return View(contents);
        }

        public async Task<ActionResult> ContactUs()
        {
            var query = new OnlineQuery();
            query.PostedDate = DateTime.Now;
            return View(query);
        }

        [HttpPost]
        public async Task<ActionResult> ContactUs(OnlineQuery query)
        {
            query.PostedDate = DateTime.Now;
            _context.Queries.Add(query);
            await _context.SaveChangesAsync();
            query = new OnlineQuery();
            query.PostedDate = DateTime.Now;
            return View(query);
        }

        public async Task<ActionResult> _PublicNavigation()
        {
            return PartialView(new MenuViewModel().GetMenus());
        }
    }
}