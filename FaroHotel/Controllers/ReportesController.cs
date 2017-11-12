using FaroHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaroHotel.Controllers
{
    public class ReportesController : Controller
    {
        private FaroHotelEntities db = new FaroHotelEntities();
        // GET: Reportes
        public ActionResult OcupacionBus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OcupacionBus(DateTime ParamFecha)
        {
            //ViewBag.pasajeros = from b in db.Bus
            //                    join ebp in db.EnlaceBusPasajero on b.ID equals ebp.BusId
            //                    join p in db.Pasajero on ebp.PasajeroId equals p.ID
            //                    where b.Fecha == ParamFecha
            //                    select new {

            //                    };
            List<GetOcupacionBuses_Result> Ocupacion = db.GetOcupacionBuses(ParamFecha).ToList();
            ViewBag.pasajerosBus1 = Ocupacion.Where(o => o.Numero == 1);
            ViewBag.pasajerosBus2 = Ocupacion.Where(o => o.Numero == 2);
            ViewBag.pasajerosBus1Count = Ocupacion.Where(o => o.Numero == 1).Count();
            ViewBag.pasajerosBus2Count = Ocupacion.Where(o => o.Numero == 2).Count();

            return PartialView("_OcupacionBus");
        }
    }
}