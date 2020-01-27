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
    public class usersController : Controller
    {
        private bshopEntities db = new bshopEntities();

        // GET: users
        [HttpPost]
        public ActionResult auth(String email,String pwd)
        {
            user client= null;
            try { client = db.users.Single(u => u.email == email && u.pwd == pwd); }
            catch(Exception ex) { return Redirect("/home/index"); }
            if (client != null)
            {
                Session["user"] = client;
            }
            return Redirect("/home/index");
        }
        public ActionResult disc()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
            }
            return Redirect("/home/index");

        }
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "image")] user user, HttpPostedFileBase image)
        {
            byte[] data = new byte[image.ContentLength];
            int status = image.InputStream.Read(data, 0, image.ContentLength);
            user.image = data;
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                if (Session["user"]!=null){
                    user u = (user)Session["user"];
                    if (u.email == "admin@gmail.com")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Redirect("/home/index");
                    }
                }
                else
                {
                    Session["user"] = user;
                    return Redirect("/home/index");
                }
               
            }

            return RedirectToAction("Index");
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,email,pwd,image,adresse,tel,cbc")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
