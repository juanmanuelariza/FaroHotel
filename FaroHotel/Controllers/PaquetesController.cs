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
    public class PaquetesController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Paquetes
        public ActionResult Index()
        {
            var paquete = db.Paquete.Include(p => p.TipoCuota).Include(p => p.TipoDescripcionPaquete).Include(p => p.TipoNoche).Include(p => p.TipoTemporada);
            return View(paquete.ToList());
        }

        // GET: Paquetes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquete.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ventanillas = db.Ventanilla.ToList();
            paquete.VentanillaIds = (from t in paquete.Ventanilla select t.ID).ToArray();

            return PartialView(paquete);
        }

        // GET: Paquetes/Create
        public ActionResult Create()
        {
            ViewBag.CuotasId = new SelectList(db.TipoCuota, "ID", "Cuotas");
            ViewBag.DescripcionId = new SelectList(db.TipoDescripcionPaquete, "ID", "Titulo");
            ViewBag.NochesId = new SelectList(db.TipoNoche, "ID", "Noches");
            ViewBag.TemporadaId = new SelectList(db.TipoTemporada, "ID", "Descripcion");

            Paquete paquete = new Paquete
            {
                VentanillaIds = new int[0]
            };

            ViewBag.Ventanillas = db.Ventanilla.ToList();
            return PartialView(paquete);
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paquete paquete)
        {
            if (ModelState.IsValid)
            {

                if (paquete.VentanillaIds != null)
                {
                    paquete.Ventanilla = (from t in db.Ventanilla.ToList() where paquete.VentanillaIds.Contains(t.ID) select t).ToList();
                }


                db.Paquete.Add(paquete);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.CuotasId = new SelectList(db.TipoCuota, "ID", "Cuotas", paquete.CuotasId);
            ViewBag.DescripcionId = new SelectList(db.TipoDescripcionPaquete, "ID", "Titulo", paquete.DescripcionId);
            ViewBag.NochesId = new SelectList(db.TipoNoche, "ID", "Noches", paquete.NochesId);
            ViewBag.TemporadaId = new SelectList(db.TipoTemporada, "ID", "Descripcion", paquete.TemporadaId);
            return PartialView(paquete);
        }

        // GET: Paquetes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquete.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuotasId = new SelectList(db.TipoCuota, "ID", "Cuotas", paquete.CuotasId);
            ViewBag.DescripcionId = new SelectList(db.TipoDescripcionPaquete, "ID", "Titulo", paquete.DescripcionId);
            ViewBag.NochesId = new SelectList(db.TipoNoche, "ID", "Noches", paquete.NochesId);
            ViewBag.TemporadaId = new SelectList(db.TipoTemporada, "ID", "Descripcion", paquete.TemporadaId);

            ViewBag.Ventanillas = db.Ventanilla.ToList();

            paquete.VentanillaIds = (from t in paquete.Ventanilla select t.ID).ToArray();


            return PartialView(paquete);
        }

        // POST: Paquetes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                Paquete paqueteOriginal = db.Paquete.Find(paquete.ID);

                paqueteOriginal.Titulo = paquete.Titulo;
                paqueteOriginal.DescripcionId = paquete.DescripcionId;
                paqueteOriginal.TemporadaId = paquete.TemporadaId;
                paqueteOriginal.NochesId = paquete.NochesId;
                paqueteOriginal.FechaInicio = paquete.FechaInicio;
                paqueteOriginal.FechaFin = paquete.FechaFin;
                paqueteOriginal.PrecioSingle = paquete.PrecioSingle;
                paqueteOriginal.PrecioDoble = paquete.PrecioDoble;
                paqueteOriginal.CuotasId = paquete.CuotasId;
                
                paqueteOriginal.Ventanilla.Clear();                                
                if (paquete.VentanillaIds != null)
                {
                    paqueteOriginal.Ventanilla = (from t in db.Ventanilla.ToList() where paquete.VentanillaIds.Contains(t.ID) select t).ToList();
                }

                db.Entry(paqueteOriginal).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.CuotasId = new SelectList(db.TipoCuota, "ID", "Cuotas", paquete.CuotasId);
            ViewBag.DescripcionId = new SelectList(db.TipoDescripcionPaquete, "ID", "Titulo", paquete.DescripcionId);
            ViewBag.NochesId = new SelectList(db.TipoNoche, "ID", "Noches", paquete.NochesId);
            ViewBag.TemporadaId = new SelectList(db.TipoTemporada, "ID", "Descripcion", paquete.TemporadaId);
            return PartialView(paquete);
        }

        //// GET: Paquetes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Paquete paquete = db.Paquete.Find(id);
        //    if (paquete == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(paquete);
        //}

        //// POST: Paquetes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Paquete paquete = db.Paquete.Find(id);
        //    db.Paquete.Remove(paquete);
        //    db.SaveChanges();
        //    return Json(new { ok = "true" });
        //}

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
