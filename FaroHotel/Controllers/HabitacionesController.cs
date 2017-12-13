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
    public class HabitacionesController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Habitaciones
        public ActionResult Index()
        {
            var habitacion = db.Habitacion.Include(h => h.TipoHotel);
            return View(habitacion.ToList());
        }

        // GET: Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return PartialView(habitacion);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.TipoHotel, "ID", "Descripcion");
            ViewBag.TiposHabitacion = db.TipoHabitacion.ToList();
            Habitacion habitacion = new Habitacion
            {
                TipoHabitacionIds = new int[0]
            };
            return PartialView(habitacion);
        }

        // POST: Habitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                if (habitacion.TipoHabitacionIds != null)
                {
                    habitacion.TipoHabitacion = (from t in db.TipoHabitacion.ToList() where habitacion.TipoHabitacionIds.Contains(t.ID) select t).ToList();
                }

                db.Habitacion.Add(habitacion);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.HotelId = new SelectList(db.TipoHotel, "ID", "Descripcion", habitacion.HotelId);
            return View(habitacion);
        }

        // GET: Habitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.TipoHotel, "ID", "Descripcion", habitacion.HotelId);

            ViewBag.TiposHabitacion = db.TipoHabitacion.ToList();
            habitacion.TipoHabitacionIds = (from h in habitacion.TipoHabitacion select h.ID).ToArray();

            return PartialView(habitacion);
        }

        // POST: Habitaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                Habitacion habitacionOriginal = db.Habitacion.Find(habitacion.ID);
                habitacionOriginal.HotelId = habitacion.HotelId;
                habitacionOriginal.Piso = habitacion.Piso;
                habitacionOriginal.Numero = habitacion.Numero;

                habitacionOriginal.TipoHabitacion.Clear();
                if (habitacion.TipoHabitacionIds != null)
                {
                    habitacionOriginal.TipoHabitacion = (from t in db.TipoHabitacion.ToList() where habitacion.TipoHabitacionIds.Contains(t.ID) select t).ToList();
                }

                db.Entry(habitacionOriginal).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.HotelId = new SelectList(db.TipoHotel, "ID", "Descripcion", habitacion.HotelId);
            return PartialView(habitacion);
        }

        // GET: Habitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitacion habitacion = db.Habitacion.Find(id);
            db.Habitacion.Remove(habitacion);
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
