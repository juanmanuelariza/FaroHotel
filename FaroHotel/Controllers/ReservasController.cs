using FaroHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaroHotel.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();
        // GET: Reservas
        public ActionResult Index()
        {
            ViewBag.NochesId = new SelectList(db.TipoNoche.OrderBy(c => c.Noches), "ID", "Noches");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Buscar(string NochesId)
        {

            ViewBag.TipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            return PartialView("_Step1");
        }

        // GET: NroHabitaciones
        [HttpGet]
        public ActionResult Habitaciones(string ParamTipoHabitacion)
        {
            int ArgTipoHabitacion = int.Parse(ParamTipoHabitacion);
            var prod = from v in db.Habitacion
                       where v.TipoHabitacion.Any(x => x.ID == ArgTipoHabitacion)
                       select v;
            var products = new SelectList(prod, "ID", "Numero");
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Step2()
        {
            return PartialView("_Step2");
        }
        public ActionResult Step3()
        {
            return PartialView("_Step3"); 

        }
        public ActionResult Step4()
        {
            return PartialView("_Step4");
        }
    }
}