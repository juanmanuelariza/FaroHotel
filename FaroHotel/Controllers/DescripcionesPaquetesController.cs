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
    public class DescripcionesPaquetesController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: DescripcionesPaquetes
        public ActionResult Index()
        {
            return View(db.TipoDescripcionPaquete.ToList());
        }

        // GET: DescripcionesPaquetes/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDescripcionPaquete tipoDescripcionPaquete = db.TipoDescripcionPaquete.Find(id);
            if (tipoDescripcionPaquete == null)
            {
                return HttpNotFound();
            }
            return PartialView(tipoDescripcionPaquete);
        }

        // GET: DescripcionesPaquetes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DescripcionesPaquetes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,FechaAlta,Activa,Titulo")] TipoDescripcionPaquete tipoDescripcionPaquete)
        {
            if (ModelState.IsValid)
            {
                tipoDescripcionPaquete.FechaAlta = DateTime.Now;
                tipoDescripcionPaquete.Activa = true;
                db.TipoDescripcionPaquete.Add(tipoDescripcionPaquete);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            return PartialView(tipoDescripcionPaquete);
        }

        // GET: DescripcionesPaquetes/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDescripcionPaquete tipoDescripcionPaquete = db.TipoDescripcionPaquete.Find(id);
            if (tipoDescripcionPaquete == null)
            {
                return HttpNotFound();
            }
            return View(tipoDescripcionPaquete);
        }

        // POST: DescripcionesPaquetes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,FechaAlta,Activa,Titulo")] TipoDescripcionPaquete tipoDescripcionPaquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDescripcionPaquete).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            return View(tipoDescripcionPaquete);
        }

        // GET: DescripcionesPaquetes/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDescripcionPaquete tipoDescripcionPaquete = db.TipoDescripcionPaquete.Find(id);
            if (tipoDescripcionPaquete == null)
            {
                return HttpNotFound();
            }
            return View(tipoDescripcionPaquete);
        }

        // POST: DescripcionesPaquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            TipoDescripcionPaquete tipoDescripcionPaquete = db.TipoDescripcionPaquete.Find(id);
            db.TipoDescripcionPaquete.Remove(tipoDescripcionPaquete);
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
