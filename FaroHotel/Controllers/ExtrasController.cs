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
    public class ExtrasController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Extras
        public ActionResult Index()
        {
            var extra = db.Extra.Include(e => e.TipoExtra);
            return View(extra.ToList());
        }

        // GET: Extras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extra.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            return PartialView(extra);
        }

        // GET: Extras/Create
        public ActionResult Create()
        {
            ViewBag.TipoExtraId = new SelectList(db.TipoExtra, "ID", "Descripcion");
            return PartialView();
        }

        // POST: Extras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TipoExtraId,FechaDesde,FechaHasta,Precio")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Extra.Add(extra);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }

            ViewBag.TipoExtraId = new SelectList(db.TipoExtra, "ID", "Descripcion", extra.TipoExtraId);
            return PartialView(extra);
        }

        // GET: Extras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extra.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoExtraId = new SelectList(db.TipoExtra, "ID", "Descripcion", extra.TipoExtraId);
            return PartialView(extra);
        }

        // POST: Extras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TipoExtraId,FechaDesde,FechaHasta,Precio")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extra).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.TipoExtraId = new SelectList(db.TipoExtra, "ID", "Descripcion", extra.TipoExtraId);
            return PartialView(extra);
        }

        // GET: Extras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extra.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            return PartialView(extra);
        }

        // POST: Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Extra extra = db.Extra.Find(id);
            db.Extra.Remove(extra);
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
