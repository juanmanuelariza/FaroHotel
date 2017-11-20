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
            List<GetOcupacionBuses_Result> Ocupacion = db.GetOcupacionBuses(ParamFecha).ToList();
            ViewBag.pasajerosBus1 = Ocupacion.Where(o => o.Numero == 1);
            ViewBag.pasajerosBus2 = Ocupacion.Where(o => o.Numero == 2);
            ViewBag.pasajerosBus1Count = Ocupacion.Where(o => o.Numero == 1).Count();
            ViewBag.pasajerosBus2Count = Ocupacion.Where(o => o.Numero == 2).Count();

            return PartialView("_OcupacionBus");
        }

        public ActionResult OcupacionHotel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OcupacionHotel(DateTime ParamFecha)
        {
            List<GetOcupacionHoteles_Result> Ocupacion = db.GetOcupacionHoteles(ParamFecha).ToList();
            ViewBag.HotelFaroI = Ocupacion.Where(h => h.HotelId == 1);
            ViewBag.HotelFaroII = Ocupacion.Where(h => h.HotelId == 2);
            ViewBag.HotelFaroICount = Ocupacion.Where(h => h.HotelId == 1).Count();
            ViewBag.HotelFaroIICount = Ocupacion.Where(h => h.HotelId == 2).Count();

            return PartialView("_OcupacionHotel");
        }
    }
}