using FaroHotel.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            AspNetUsers usuario = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<Paquete> paquetes = db.Paquete.Where(p => p.FechaInicio <= ParamFecha && p.FechaFin >= ParamFecha && p.Ventanilla.Any(v => v.ID == usuario.VentanillaId)).ToList();       
            ViewBag.SelectPaquetesDisponibles = new SelectList(paquetes, "ID", "Titulo");
            return PartialView("_Step2");
        }
        public ActionResult Step3(DateTime ParamFecha)
        {
            List<Extra> extras = db.Extra.Where(e => e.FechaDesde <= ParamFecha && e.FechaHasta >= ParamFecha).ToList();

            foreach (Extra extra in extras){
                switch (extra.TipoExtraId)
                {
                    case 1: //Vouchers
                        ViewBag.PrecioVouchers = extra.Precio;
                        break;
                    case 2: //Cuna
                        ViewBag.PrecioCuna = extra.Precio;
                        break;
                    case 3: //Cochera en Faro II
                        ViewBag.PrecioCocheraFaroII = extra.Precio;
                        break;
                    default:
                        break;
                }
            }


            return PartialView("_Step3");

        }
        public ActionResult Step4(DateTime ParamFecha, int ParamHotelId, int ParamNochesId, int ParamTitularId, int[] ParamTipoHabitacionesIds, int[] ParamHabitacionesIds, int[] ParamPasajerosIds, int[] ParamPaquetesIds, int[] ParamBaseIds, int[] ParamBusIdaIds, int[] ParamAsientosIdaIds, int[] ParamBusVueltaIds, int[] ParamAsientosVueltaIds, int[] ParamExtrasIds, int[] ParamExtrasCantidad)
        {
            InsertBooking_Result result = db.InsertBooking(ParamFecha, ParamHotelId, ParamNochesId, ParamTitularId,
                string.Join(",", ParamTipoHabitacionesIds), string.Join(",", ParamHabitacionesIds),
                string.Join(",", ParamPasajerosIds), string.Join(",", ParamPaquetesIds), string.Join(",", ParamBaseIds),
                User.Identity.GetUserId(), string.Join(",", ParamBusIdaIds), string.Join(",", ParamAsientosIdaIds),
                string.Join(",", ParamBusVueltaIds), string.Join(",", ParamAsientosVueltaIds), 
                string.Join(",", ParamExtrasIds), string.Join(",", ParamExtrasCantidad)).FirstOrDefault();
            ViewBag.NroReserva = result.ReservaHotelID;

            try
            {
                //Envio Mail
                SendMail mail = new SendMail();
                mail.Destinatario = "juanma.ariza23@gmail.com";
                mail.Asunto = "Test2";
                mail.Mensaje = "Nueva reserva!!";
                mail.Send();
            }
            catch (Exception)
            {

                throw;
            }

            return PartialView("_Step4");
        }       


        public ActionResult Bus(DateTime ParamFecha, int ParamTrayectoId, int ParamNoches)
        {
            //Si es Trayecto de vuelta, sumo los dias del paquete más uno
            //if (ParamTrayectoId == 2) 
            //{
            //    ParamFecha = ParamFecha.AddDays(ParamNoches + 1);
            //}
            //ViewBag.Buses = from b in db.Bus
            //                where b.Fecha == ParamFecha && b.TrayectoId == ParamTrayectoId
            //                select b;
            List<GetBuses_Result> buses = db.GetBuses(ParamFecha, ParamNoches).ToList();
            
            ViewBag.Buses = buses;

            //ViewBag.BusesIds = from b in db.Bus
            //                where b.Fecha == ParamFecha && b.TrayectoId == ParamTrayectoId
            //                select b.ID;

            DateTime FechaVuelta = ParamFecha.AddDays(ParamNoches + 1);

            ViewBag.BusesIdaYVueltaIds = from b in db.Bus
                                         where b.Fecha == ParamFecha || b.Fecha == FechaVuelta
                                         select b.ID;
            string swResult = "";
            switch (buses.First().TipoBusId)
            {
                case 1: //Bus60-2017
                    swResult = "_Bus60-2017";
                    break;
                case 2: //Bus62-2017
                    swResult = "_Bus62-2017";
                    break;
                default:
                    swResult = "_Bus62-2017";
                    break;
            }
            return PartialView(swResult);
        }

        // GET: NroHabitaciones
        [HttpPost]
        public ActionResult Habitaciones(DateTime ParamFecha, int ParamHotelId, int ParamTipoHabitacion)
        {
            var rslt = db.GetHabitacionesDisponibles(ParamFecha, ParamHotelId, ParamTipoHabitacion).Select(h => new
            {
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

        // Post: OcupacionBus
        [HttpPost]
        public ActionResult OcupacionBus(int[] ParamBusesIds)
        {
            var rslt = from ob in db.EnlaceBusPasajero
                       where ParamBusesIds.Contains(ob.BusId)
                       select new
                       {
                           BusId = ob.BusId,
                           NroButaca = ob.NroButaca,
                           Numero = ob.Bus.Numero
                       }; 
            return Json(rslt);
        }

        //[HttpPost]
        //public ActionResult OcupacionBus(DateTime ParamFecha, int ParamNoches)
        //{
        //    List<GetBuses_Result> buses = db.GetBuses(ParamFecha, ParamNoches).ToList();

        //    return Json(rslt);
        //}




        // GET: Paquetes
        [HttpGet]
        public ActionResult Paquetes(int ParamPaqueteId)
        {
            var tmp_paquete = (from p in db.Paquete
                               where p.ID == ParamPaqueteId
                               select new
                               {
                                   BusIda = p.BusIda,
                                   BusVuelta = p.BusVuelta,
                                   PrecioSingle = p.PrecioSingle,
                                   PrecioDoble = p.PrecioDoble

                               });
            return Json(tmp_paquete, JsonRequestBehavior.AllowGet);
        }

        //Reservas/Confirmar
        public ActionResult Listado()
        {
            var reservas = db.ReservaHotel.Where(r => r.Confirmada == true);
            return View(reservas.ToList());
        }

        public ActionResult Solicitudes()
        {
            var reservas = db.ReservaHotel.Where(r => r.Confirmada == false);
            return View(reservas.ToList());
        }

        public ActionResult Detalle(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservaHotel reserva = db.ReservaHotel.Find(Id);
            ViewBag.ReservaId = reserva.ID;
            ViewBag.Confirmada = reserva.Confirmada;
            ViewBag.DatosReserva = db.GetReserva(Id);
            ViewBag.HabitacionesReservadas = db.GetReservaHabitaciones(Id);
            ViewBag.PasajerosReserva = db.GetReservaPasajeros(Id);
            ViewBag.Fecha = reserva.FechaEntrada.ToShortDateString();
            ViewBag.Noches = (reserva.FechaSalida - reserva.FechaEntrada).Days;

            AspNetUsers usuario = db.AspNetUsers.Find(User.Identity.GetUserId());
            List<Paquete> paquetes = db.Paquete.Where(p => p.FechaInicio <= reserva.FechaEntrada && p.FechaFin >= reserva.FechaEntrada && p.Ventanilla.Any(v => v.ID == usuario.VentanillaId)).ToList();
            ViewBag.SelectPaquetesDisponibles = new SelectList(paquetes, "ID", "Titulo");


            return View();
        }

        public ActionResult Confirmar(int? id)
        {
            ViewBag.ReservaId = id;
            return PartialView("_Confirmar");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            try
            {
                ReservaHotel reserva = db.ReservaHotel.Find(id);
                reserva.Confirmada = true;
                reserva.UsuarioConfirma = User.Identity.GetUserId();
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            catch
            {
                return Json(new { ok = "false" });
            }
                     
        }

        public ActionResult AgregarHabitacion(int? Id)
        {
            ReservaHotel reserva = db.ReservaHotel.Find(Id);
            ViewBag.ReservaId = Id;
            ViewBag.Fecha = reserva.FechaEntrada;
            ViewBag.HotelId = reserva.HotelId;
            ViewBag.SelectTipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            return PartialView("_AgregarHabitacion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarHabitacion(int id, int TipoHabitacionId, int HabitacionId)
        {
            try
            {
                EnlaceReservaHotelHabitacion habitacion = new EnlaceReservaHotelHabitacion();
                habitacion.ReservaHotelId = id;
                habitacion.TipoHabitacionId = TipoHabitacionId;
                habitacion.HabitacionId = HabitacionId;
                db.EnlaceReservaHotelHabitacion.Add(habitacion);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            catch
            {
                return Json(new { ok = "false" });
            }

        }

        public ActionResult EditarHabitacion(int? Id)
        {
            EnlaceReservaHotelHabitacion habitacion = db.EnlaceReservaHotelHabitacion.Where(h => h.ID == Id).First();            
            ViewBag.Id = habitacion.ID;
            ViewBag.Fecha = habitacion.ReservaHotel.FechaEntrada;
            ViewBag.HotelId = habitacion.ReservaHotel.HotelId;
            ViewBag.SelectTipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");

            //var habitacionesDisponibles = db.GetHabitacionesDisponibles(habitacion.ReservaHotel.FechaEntrada, habitacion.ReservaHotel.HotelId, habitacion.TipoHabitacionId).Select(h => new
            //{
            //    ID = h.ID,
            //    Numero = h.Numero
            //}).ToList();

            //ViewBag.SelectNumHabitacion = new SelectList(habitacionesDisponibles, "ID", "Descripcion");
            return PartialView("_EditarHabitacion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarHabitacion(int id, int TipoHabitacionId, int HabitacionId)
        {
            try
            {
                EnlaceReservaHotelHabitacion habitacion = db.EnlaceReservaHotelHabitacion.Where(h => h.ID == id).First();
                EnlaceReservaHotelHabitacion habitacionNueva = new EnlaceReservaHotelHabitacion();
                //Copio objeto
                habitacionNueva.ReservaHotelId = habitacion.ReservaHotelId;
                habitacionNueva.TipoHabitacionId = TipoHabitacionId;
                habitacionNueva.HabitacionId = HabitacionId;
                //Elimino objeto viejo y creo el nuevo
                db.EnlaceReservaHotelHabitacion.Remove(habitacion);
                db.EnlaceReservaHotelHabitacion.Add(habitacionNueva);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            catch
            {
                return Json(new { ok = "false" });
            }
        }

        public ActionResult EliminarHabitacion(int? Id)
        {
            ViewBag.HabitacionId = Id;
            return PartialView("_EliminarHabitacion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarHabitacion(int id)
        {
            try
            {
                EnlaceReservaHotelHabitacion habitacion = db.EnlaceReservaHotelHabitacion.Where(h => h.ID == id).First();
                db.EnlaceReservaHotelHabitacion.Remove(habitacion);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            catch (Exception)
            {
                return Json(new { ok = "false" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarPasajero(int ParamReservaId, int paramPasajeroId, int paramPaqueteId, int paramBaseId, int paramBusIdaId, int paramAsientoIda, int paramBusVueltaId, int paramAsientoVuelta)
        {
            try
            {
                AgregarPasajeroAReserva_Result respuesta = db.AgregarPasajeroAReserva(ParamReservaId, paramPasajeroId, paramPaqueteId, paramBaseId, paramBusIdaId, paramAsientoIda, paramBusVueltaId, paramAsientoVuelta).FirstOrDefault();
                if (respuesta.Resultado == 0)
                {
                    return Json(new { ok = "false" });
                }
                else
                {
                    return Json(new { ok = "true" });
                }
            }
            catch (Exception)
            {
                return Json(new { ok = "false" });
            }
        }

        [HttpPost]
        public ActionResult EliminarPasajero(int ParamReservaId, int ParamPasajeroId)
        {
            //Recibo Id de reserva
            ViewBag.ReservaId = ParamReservaId;
            ViewBag.PasajeroId = ParamPasajeroId;
            return PartialView("_EliminarPasajero");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarPasajeroConfirmado(int ParamReservaId, int ParamPasajeroId)
        {
            try
            {
                EliminarPasajeroDeReserva_Result respuesta = db.EliminarPasajeroDeReserva(ParamReservaId, ParamPasajeroId).FirstOrDefault();
                if (respuesta.Resultado == 0)
                {
                    return Json(new { ok = "false" });
                }
                else
                {
                    return Json(new { ok = "true" });
                }
            }
            catch (Exception)
            {
                return Json(new { ok = "false" });
            }
        }
    }
}