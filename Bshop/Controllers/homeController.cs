using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bshop.Models;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace Bshop.Controllers
{
    public class homeController : Controller
    {
        private bshopEntities db = new bshopEntities();
        // GET: home

        public ActionResult Index()
        {
            return View(db.livres.ToList());
        }
      
        public ActionResult panier()
        {
            List<List<Object>> mycart = new List<List<Object>>();
            if (Session["user"] != null)
            {
                user u = (user)Session["user"];
                List<panier> p=db.paniers.Where(en =>en.idc==u.Id).ToList();
            
                foreach(var i in p)
                {
                    livre l = db.livres.Find(i.idl);
                    mycart.Add(new List<Object> { i, l });
                }
            }
            ViewData["panier"] = mycart;
            return View();
        }
        [HttpGet]
        public ActionResult addPanier(int idc,int idl,int qte)
        {
            panier p = new panier();
            p.idc = idc;
            p.idl = idl;
            p.qte = qte;
            if (ModelState.IsValid)
            {
                db.paniers.Add(p);
                db.SaveChanges();
                livre livre =db.livres.Find(idl);
                livre.stock = livre.stock - qte;
                db.Entry(livre).State = EntityState.Modified;
                db.SaveChanges();
                
            }
           
            return RedirectToAction("panier");
        }
[HttpGet]
public ActionResult deletePanier(int id,int idl, int qte)
        {
            if (ModelState.IsValid)
            {
               
                livre livre = db.livres.Find(idl);
                livre.stock = livre.stock + qte;
                db.Entry(livre).State = EntityState.Modified;
                db.SaveChanges();
                panier pp = db.paniers.Find(id);
                db.paniers.Remove(pp);
                db.SaveChanges();

            }
            return RedirectToAction("panier");
        }
        [HttpPost]
        public ActionResult search(String recherche)
        {
            return View(db.livres.Where(livre => livre.titre.ToLower().Contains(recherche.ToLower())));
        }

        // GET: home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
