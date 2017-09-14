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
        public ActionResult Buscar(string ParamFecha, int ParamNochesId, int ParamHabitaciones, int ParamPasajeros)
        {
            ViewBag.NochesId = ParamNochesId;
            ViewBag.CantHabitaciones = ParamHabitaciones;
            ViewBag.CantPasajeros = ParamPasajeros;
            ViewBag.TipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            return PartialView("_Step1");
        }

        // GET: NroHabitaciones
        [HttpGet]
        public ActionResult Habitaciones(string ParamTipoHabitacion)
        {
            int ArgTipoHabitacion = int.Parse(ParamTipoHabitacion);
            var tmp_room = (from h in db.Habitacion
                            where h.TipoHabitacion.Any(x => x.ID == ArgTipoHabitacion)
                            select new
                            {
                                ID = h.ID,
                                Numero = h.Numero
                            });                        
            return Json(tmp_room, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Step2(string ParamFecha, int ParamNochesId, int ParamHabitaciones, int ParamPasajeros)
        {
            ViewBag.NochesId = ParamNochesId;
            ViewBag.CantHabitaciones = ParamHabitaciones;
            ViewBag.CantPasajeros = ParamPasajeros;
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

        public ActionResult Bus()
        {
            return PartialView("_Busqueda");
        }
        
        [HttpGet]
        public JsonResult BuscarPax(string ParamDNI)
        {
            int ArgDNI = int.Parse(ParamDNI);
            var tmp_Pax = (from p in db.Pasajero
                           where p.DNI == ArgDNI
                           select new
                           {
                               ID = p.ID,
                               Apellido = p.Apellido,
                               Nombre = p.Nombre
                           });


            //db.Pasajero.Where(p => p.DNI == ArgDNI).Select();
            return Json(tmp_Pax, JsonRequestBehavior.AllowGet);
        }
        
    }
}