using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaroHotel.Controllers
{
    public class ReservasController : Controller
    {
        // GET: Reservas
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Buscar()
        {
            return PartialView("_Busqueda");
        }
    }
}