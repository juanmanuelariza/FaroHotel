using FaroHotel.Models;
using Microsoft.AspNet.Identity;
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
            ViewBag.SelectTipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            return PartialView("_Step1");
        }


        public ActionResult Step2(DateTime ParamFecha, int ParamNochesId, int ParamHabitaciones, int ParamPasajeros)
        {            
            ViewBag.NochesId = ParamNochesId;
            ViewBag.CantHabitaciones = ParamHabitaciones;
            ViewBag.CantPasajeros = ParamPasajeros;
            ViewBag.SelectPaquetesDisponibles = new SelectList(db.Paquete.Where(p => p.FechaInicio <= ParamFecha && p.FechaFin >= ParamFecha) , "ID", "Titulo");
            return PartialView("_Step2");
        }
        public ActionResult Step3()
        {
            return PartialView("_Step3"); 

        }
        public ActionResult Step4(DateTime ParamFecha, int ParamHotelId, int ParamNochesId, int ParamTitularId, int[] ParamTipoHabitacionesIds, int[] ParamNroHabitacionesIds, int[] ParamPasajerosIds, int[] ParamPaquetesIds, int[] ParamBusIdaIds, int[] ParamAsientosIdaIds, int[] ParamBusVueltaIds, int[] ParamAsientosVueltaIds)        
        {
            InsertBooking_Result result = db.InsertBooking(ParamFecha, ParamHotelId, ParamNochesId, ParamTitularId, 
                string.Join(",", ParamTipoHabitacionesIds) , string.Join(",", ParamNroHabitacionesIds), 
                string.Join(",", ParamPasajerosIds), string.Join(",", ParamPaquetesIds), 
                User.Identity.GetUserId(), string.Join(",", ParamBusIdaIds), string.Join(",", ParamAsientosIdaIds), 
                string.Join(",", ParamBusVueltaIds), string.Join(",", ParamAsientosVueltaIds)).FirstOrDefault();
            ViewBag.NroReserva = result.ReservaHotelID;
            return PartialView("_Step4");
        }

        public ActionResult Bus(DateTime ParamFecha)
        {
            var buses = from b in db.Bus where b.Fecha == ParamFecha
                        select b.ID;
            ViewBag.BusesIds = buses.ToList();
            return PartialView("_Bus");
        }

        // GET: NroHabitaciones
        [HttpPost]
        public ActionResult Habitaciones(DateTime ParamFecha, int ParamHotelId, int ParamTipoHabitacion)
        {   
            var rslt = db.GetHabitacionesDisponibles(ParamFecha, ParamHotelId, ParamTipoHabitacion).Select(h => new {
                ID = h.ID,
                Numero = h.Numero
            }).ToList();
            return Json(rslt);
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

        // Post: NroHabitaciones
        [HttpPost]
        public ActionResult OcupacionBus(int[] ParamBusesIds)
        {
            var rslt = from ob in db.EnlaceBusPasajero
                       where ParamBusesIds.Contains(ob.BusId)
                       select new {
                           BusId = ob.BusId,
                           NroButaca = ob.NroButaca
                       };
            return Json(rslt);
        }

        

        // GET: NroHabitaciones
        [HttpGet]
        public ActionResult Paquetes(int ParamPaqueteId)
        {
            var tmp_paquete = (from p in db.Paquete
                            where p.ID == ParamPaqueteId
                            select new
                            {
                                BusIda = p.BusIda,
                                BusVuelta = p.BusVuelta
                            });
            return Json(tmp_paquete, JsonRequestBehavior.AllowGet);
        }
    }
}