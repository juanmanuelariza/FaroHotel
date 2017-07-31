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
    public class UsuariosController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.Ventanilla);
            return View(aspNetUsers.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetUsers);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Ventanilla = new SelectList(db.Ventanilla, "ID", "Nombre");
            return PartialView();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,VentanillaId")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VentanillaId = new SelectList(db.Ventanilla, "ID", "Nombre", aspNetUsers.VentanillaId);
            return PartialView(aspNetUsers);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            //UsuarioVM user = new UsuarioVM();
            //user.Id = aspNetUsers.Id;
            //user.UserName = aspNetUsers.UserName;
            //user.Password = aspNetUsers.PasswordHash;
            //user.Email = aspNetUsers.Email;
            //user.PhoneNumber = aspNetUsers.PhoneNumber;
            //user.VentanillaId = aspNetUsers.VentanillaId;

            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentanillaId = new SelectList(db.Ventanilla, "ID", "Nombre", aspNetUsers.VentanillaId);
            return PartialView(aspNetUsers);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,VentanillaId")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            ViewBag.VentanillaId = new SelectList(db.Ventanilla, "ID", "Nombre", aspNetUsers.VentanillaId);
            return PartialView(aspNetUsers);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentanillaId = new SelectList(db.Ventanilla, "ID", "Nombre", aspNetUsers.VentanillaId);
            return PartialView(aspNetUsers);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
