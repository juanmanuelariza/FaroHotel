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
    public class BusesController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Buses
        public ActionResult Index()
        {
            var bus = db.Bus.Include(b => b.TipoTrayecto);
            return View(bus.ToList().OrderByDescending(b => b.ID));
        }

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return PartialView(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            ViewBag.TrayectoId = new SelectList(db.TipoTrayecto, "ID", "Descripcion");
            return PartialView();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Fecha,TrayectoId")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Bus.Add(bus);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.TrayectoId = new SelectList(db.TipoTrayecto, "ID", "Descripcion", bus.TrayectoId);
            return PartialView(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrayectoId = new SelectList(db.TipoTrayecto, "ID", "Descripcion", bus.TrayectoId);
            return PartialView(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Fecha,TrayectoId")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.TrayectoId = new SelectList(db.TipoTrayecto, "ID", "Descripcion", bus.TrayectoId);
            return PartialView(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return PartialView(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = db.Bus.Find(id);
            db.Bus.Remove(bus);
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
