using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FaroHotel.Models;

namespace FaroHotel.Controllers
{
    
    public class MenuController : Controller
    {
        FaroHotelEntities Context = new FaroHotelEntities();

        // GET: Menu
        public List<MenuVM> GetMenu()
        {
            var datos = Context.Menu.Where(r=>r.Activo).Select(r=> new MenuVM() {
                ID = r.ID,
                PadreID = r.PadreID,
                Nombre=r.Nombre,
                Accion=r.Accion,
                Controlador=r.Controlador,
                Icono = r.Icono
            }).ToList();

            return datos;
        }

        public List<MenuVM> GetMenu(String UserName)
        {
            var datos = Context.GetPermisosPorNombreDeUsuario(UserName).Select(r => new MenuVM()
            {
                ID = r.ID,
                PadreID = r.PadreID,
                Nombre = r.Nombre,
                Accion = r.Accion,
                Controlador = r.Controlador,
                Icono = r.Icono
            }).ToList();

            return datos;
        }
    }
}
