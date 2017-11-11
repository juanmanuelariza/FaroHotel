﻿using FaroHotel.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            ViewBag.SelectPaquetesDisponibles = new SelectList(db.Paquete.Where(p => p.FechaInicio <= ParamFecha && p.FechaFin >= ParamFecha), "ID", "Titulo");
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
        public ActionResult Step4(DateTime ParamFecha, int ParamHotelId, int ParamNochesId, int ParamTitularId, int[] ParamTipoHabitacionesIds, int[] ParamNroHabitacionesIds, int[] ParamPasajerosIds, int[] ParamPaquetesIds, int[] ParamBaseIds, int[] ParamBusIdaIds, int[] ParamAsientosIdaIds, int[] ParamBusVueltaIds, int[] ParamAsientosVueltaIds, int[] ParamExtrasIds, int[] ParamExtrasCantidad)
        {
            InsertBooking_Result result = db.InsertBooking(ParamFecha, ParamHotelId, ParamNochesId, ParamTitularId,
                string.Join(",", ParamTipoHabitacionesIds), string.Join(",", ParamNroHabitacionesIds),
                string.Join(",", ParamPasajerosIds), string.Join(",", ParamPaquetesIds), string.Join(",", ParamBaseIds),
                User.Identity.GetUserId(), string.Join(",", ParamBusIdaIds), string.Join(",", ParamAsientosIdaIds),
                string.Join(",", ParamBusVueltaIds), string.Join(",", ParamAsientosVueltaIds), 
                string.Join(",", ParamExtrasIds), string.Join(",", ParamExtrasCantidad)).FirstOrDefault();
            ViewBag.NroReserva = result.ReservaHotelID;
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

            return PartialView("_Bus");
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
        public ActionResult AgregarHabitacion(int id, int TipoHabitacion, int NumHabitacion)
        {
            try
            {
                EnlaceReservaHotelHabitacion habitacion = new EnlaceReservaHotelHabitacion();
                habitacion.ReservaHotelId = id;
                habitacion.TipoHabitacionId = TipoHabitacion;
                habitacion.HabitacionId = NumHabitacion;
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
            ReservaHotel reserva = db.ReservaHotel.Find(Id);
            ViewBag.ReservaId = Id;
            ViewBag.Fecha = reserva.FechaEntrada;
            ViewBag.HotelId = reserva.HotelId;
            ViewBag.SelectTipoHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            ViewBag.SelectNumHabitacion = new SelectList(db.TipoHabitacion.OrderBy(c => c.ID), "ID", "Descripcion");
            return PartialView("_AgregarHabitacion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarHabitacion(int id, int TipoHabitacion, int NumHabitacion)
        {
            try
            {
                EnlaceReservaHotelHabitacion habitacion = new EnlaceReservaHotelHabitacion();
                habitacion.ReservaHotelId = id;
                habitacion.TipoHabitacionId = TipoHabitacion;
                habitacion.HabitacionId = NumHabitacion;
                db.EnlaceReservaHotelHabitacion.Add(habitacion);
                db.SaveChanges();
                return Json(new { ok = "true" });
            }
            catch
            {
                return Json(new { ok = "false" });
            }

        }
    }            
}