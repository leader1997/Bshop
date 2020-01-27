using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bshop.Models;

namespace Bshop.Controllers
{
    public class livresController : Controller
    {
        private bshopEntities db = new bshopEntities();

        // GET: livres
        public ActionResult Index()
        {
            return View(db.livres.ToList());
        }

        // GET: livres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            livre livre = db.livres.Find(id);
            if (livre == null)
            {
                return HttpNotFound();
            }
            return View(livre);
        }

        // GET: livres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: livres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "image")]  livre livre, HttpPostedFileBase image)
        {
            byte[] data = new byte[image.ContentLength];
            int status = image.InputStream.Read(data, 0, image.ContentLength);
            livre.image = data;
            if (ModelState.IsValid)
            {
                db.livres.Add(livre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livre);
        }

        // GET: livres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            livre livre = db.livres.Find(id);
            if (livre == null)
            {
                return HttpNotFound();
            }
            return View(livre);
        }

        // POST: livres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "image")]  livre livre, HttpPostedFileBase image)
        {
            byte[] data = new byte[image.ContentLength];
            int status = image.InputStream.Read(data, 0, image.ContentLength);
            livre.image = data;

            if (ModelState.IsValid)
            {
                db.Entry(livre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livre);
        }

        // GET: livres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            livre livre = db.livres.Find(id);
            if (livre == null)
            {
                return HttpNotFound();
            }
            return View(livre);
        }

        // POST: livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            livre livre = db.livres.Find(id);
            db.livres.Remove(livre);
            db.SaveChanges();
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
