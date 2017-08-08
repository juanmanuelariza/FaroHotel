using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FaroHotel.Models;

namespace FaroHotel.Controllers
{
    [Authorize]
    public class VentanillasController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Ventanillas
        public ActionResult Index()
        {
            return View(db.Ventanilla.ToList());
        }

        // GET: Ventanillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventanilla ventanilla = db.Ventanilla.Find(id);
            if (ventanilla == null)
            {
                return HttpNotFound();
            }
            return View(ventanilla);
        }

        // GET: Ventanillas/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Ventanillas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Direccion,Telefono,Email")] Ventanilla ventanilla)
        {
            if (ModelState.IsValid)
            {
                ventanilla.FechaAlta = DateTime.Now;
                db.Ventanilla.Add(ventanilla);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }            
            return PartialView(ventanilla);
        }

        // GET: Ventanillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventanilla ventanilla = db.Ventanilla.Find(id);
            if (ventanilla == null)
            {
                return HttpNotFound();
            }
            return PartialView(ventanilla);
        }

        // POST: Ventanillas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Direccion,Telefono,Email,FechaAlta")] Ventanilla ventanilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventanilla).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            return PartialView(ventanilla);
        }

        // GET: Ventanillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventanilla ventanilla = db.Ventanilla.Find(id);
            if (ventanilla == null)
            {
                return HttpNotFound();
            }
            return PartialView(ventanilla);
        }

        // POST: Ventanillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ventanilla ventanilla = db.Ventanilla.Find(id);
            db.Ventanilla.Remove(ventanilla);
            db.SaveChanges();
            return Json(new { ok = "true" });
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
