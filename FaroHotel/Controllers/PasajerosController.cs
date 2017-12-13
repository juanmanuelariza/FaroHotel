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
    public class PasajerosController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Pasajeros
        public ActionResult Index()
        {
            var pasajero = db.Pasajero.Include(p => p.TipoSexo);
            return View(pasajero.ToList());
        }

        // GET: Pasajeros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasajero pasajero = db.Pasajero.Find(id);
            if (pasajero == null)
            {
                return HttpNotFound();
            }
            return PartialView(pasajero);
        }

        // GET: Pasajeros/Create
        public ActionResult Create()
        {
            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion");
            return PartialView();
        }

        // POST: Pasajeros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DNI,Apellido,Nombre,Sexo,FechaNacimiento,Telefono,Email,Diabetes,Celiaquia,Motricidad,ListaNegra")] Pasajero pasajero)
        {
            if (ModelState.IsValid)
            {
                List<Pasajero> pasajeroEnBD = db.Pasajero.Where(p => p.DNI == pasajero.DNI).ToList();
                if (pasajeroEnBD.Count > 0)
                {
                    return Json(new { ok = "false" });
                }
                db.Pasajero.Add(pasajero);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion", pasajero.Sexo);
            return PartialView(pasajero);
        }

        // GET: Pasajeros/Create
        public ActionResult Create2()
        {
            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion");
            return PartialView();
        }

        // POST: Pasajeros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "ID,DNI,Apellido,Nombre,Sexo,FechaNacimiento,Telefono,Email,Diabetes,Celiaquia,Motricidad,ListaNegra")] Pasajero pasajero)
        {
            if (ModelState.IsValid)
            {
                List<Pasajero> pasajeroEnBD = db.Pasajero.Where(p => p.DNI == pasajero.DNI).ToList();
                if (pasajeroEnBD.Count > 0)
                {
                    return Json(new { ok = "false" });
                }
                pasajero.ListaNegra = false;
                db.Pasajero.Add(pasajero);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion", pasajero.Sexo);
            return PartialView(pasajero);
        }


        // GET: Pasajeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasajero pasajero = db.Pasajero.Find(id);
            if (pasajero == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion", pasajero.Sexo);
            return PartialView(pasajero);
        }

        // POST: Pasajeros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DNI,Apellido,Nombre,Sexo,FechaNacimiento,Telefono,Email,Diabetes,Celiaquia,Motricidad,ListaNegra")] Pasajero pasajero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pasajero).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.Sexo = new SelectList(db.TipoSexo, "ID", "Descripcion", pasajero.Sexo);
            return PartialView(pasajero);
        }

        // GET: Pasajeros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasajero pasajero = db.Pasajero.Find(id);
            if (pasajero == null)
            {
                return HttpNotFound();
            }
            return PartialView(pasajero);
        }

        // POST: Pasajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pasajero pasajero = db.Pasajero.Find(id);
            db.Pasajero.Remove(pasajero);
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
