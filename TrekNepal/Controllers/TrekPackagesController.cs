using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrekNepal.Models;

namespace TrekNepal.Controllers
{
    public class TrekPackagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrekPackages
        public async Task<ActionResult> Index()
        {
            return View(await db.Packages.ToListAsync());
        }

        // GET: TrekPackages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrekPackage trekPackage = await db.Packages.FindAsync(id);
            if (trekPackage == null)
            {
                return HttpNotFound();
            }
            return View(trekPackage);
        }

        // GET: TrekPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrekPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PackageTitle,PackageType,PackageDuration,DurationInWord,Difficulty,PackagePrice")] TrekPackage trekPackage)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(trekPackage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(trekPackage);
        }

        // GET: TrekPackages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrekPackage trekPackage = await db.Packages.FindAsync(id);
            if (trekPackage == null)
            {
                return HttpNotFound();
            }
            return View(trekPackage);
        }

        // POST: TrekPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PackageTitle,PackageType,PackageDuration,DurationInWord,Difficulty,PackagePrice")] TrekPackage trekPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trekPackage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trekPackage);
        }

        // GET: TrekPackages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrekPackage trekPackage = await db.Packages.FindAsync(id);
            if (trekPackage == null)
            {
                return HttpNotFound();
            }
            return View(trekPackage);
        }

        // POST: TrekPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TrekPackage trekPackage = await db.Packages.FindAsync(id);
            db.Packages.Remove(trekPackage);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
