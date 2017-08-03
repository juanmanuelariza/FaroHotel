using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FaroHotel.Models;
using System.Web.Mvc;

namespace FaroHotel.Helpers
{
    public static class HtmlHelperExtensions
    {
        //Renderizado de Menu(Panel Izquerdo)
        #region Renderizado de Menu(Panel Izquerdo)
        public static MvcHtmlString RenderMenu(List<MenuVM> menus)//this HtmlHelper helper, 
        {            
            var menusPadres = menus == null ? null : menus.Where(r => r.PadreID == null).ToList();
            return MvcHtmlString.Create(createMenu(menusPadres, menus));
        }

        private static string createMenu(List<MenuVM> menusPadres, List<MenuVM> menusAll, string output = null)
        {
            if (menusPadres != null && menusAll != null)
            {

                if (menusPadres.Any(r => r.PadreID != null))//Son opciones de menu q tienen un padre
                {
                    foreach (var item in menusPadres)
                    {

                        if (menusAll.Where(r => r.PadreID == item.ID).Any())
                        {
                            //< ul class="nav side-menu">
                            //    <li>
                            //        <a><i class="fa fa-cogs"></i>ABM<span class="fa fa-chevron-down"></span></a>
                            //        <ul class="nav child_menu">
                            //            <li><a href = "/Usuarios" >< i class="fa fa-user"></i>Usuarios</a></li>
                            //            <li><a href = "/Pasajeros" >< i class="fa fa-users"></i>Pasajeros</a></li>
                            //            <li><a href = "/Ventanillas" >< i class="fa fa-square-o"></i>Ventanillas</a></li>
                            //            <li><a href = "/Paquetes" >< i class="fa fa-ticket"></i>Paquetes</a></li>
                            //        </ul>
                            //    </li>
                            //</ul>
                            string a = string.Format(@"<li><a><i class=""{1}""></i> {0} <span class=""fa fa-chevron-down""></span></a><ul class=""nav child_menu"">", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o": item.Icono);
                            output += a;

                            output = createMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

                            string b = @"</li>";
                            output += b;
                        }
                        else
                        {
                            string accion = string.IsNullOrEmpty(item.Accion) ? "" : item.Accion;
                            string url = "";

#if (DEBUG == false)
                            url = "/SRProf";
#endif
                            url += "/" + item.Controlador + "/" + (accion=="Index"?String.Empty:accion);
                            string c = string.Format(@"<li><a href=""{1}""><i class=""{2}""></i> {0} </a></li>", item.Nombre, url, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o": item.Icono);
                            output += c;
                        }
                    }
                    //return output;
                }
                else
                {

                    foreach (var item in menusPadres)
                    {
                        string x = string.Format(@"<ul class=""nav side-menu""><li><a><i class=""{1}""></i><span> {0} </span><span class=""fa fa-chevron-down""></span></a>", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o": item.Icono);
                        output += x;

                        if (menusAll.Where(r => r.PadreID == item.ID).Any())
                        {
                            string y = @"<ul class=""nav child_menu"">";
                            output += y;

                            output = createMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

                            string z = @"</ul>";
                            output += z;
                        }
                        output += "</li></ul>";

                    }

                }
                return output;
            }
            else
            {
                return @"<li><a href=""#""><i class=""fa fa-circle-o text-red""></i> <span>No hay menus disponibles</span></a></li>";
            }
        }
        #endregion


        //Renderizado de Permisos de Menu y acciones(JSTree)
        #region Renderizado de Permisos de Menu y acciones(JSTree)
        public static MvcHtmlString RenderTreeMenu(List<Menu> menus)//this HtmlHelper helper, 
        {
            var menusPadres = menus == null ? null : menus.Where(r => r.PadreID == null).ToList();
            return MvcHtmlString.Create(createTreeMenu(menusPadres, menus));
        }

        private static string createTreeMenu(List<Menu> menusPadres, List<Menu> menusAll, string output = null)
        {
            if (menusPadres != null && menusAll != null)
            {

                //
                if (menusPadres.Any(r => r.PadreID != null))//Son opciones de menu q tienen un padre
                {
                    foreach (var item in menusPadres)
                    {
                        

                        if (menusAll.Where(r => r.PadreID == item.ID).Any())
                        {
                            string a = string.Format(@"<li id=""{2}"" data-jstree='{{""icon"":""{1}""}}'> {0} <ul>", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o" : item.Icono, "menu_"+item.ID);
                            output += a;

                            output = createTreeMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

                            string b = @"</ul></li>";
                            output += b;
                        }
                        else
                        {
                            //string accion = string.IsNullOrEmpty(item.Accion) ? "" : item.Accion;
                            //string url = "";

                        #if (DEBUG == false)
                            url = "/SRProf";
                        #endif

                            //url += "/" + item.Controlador + "/" + (accion == "Index" ? String.Empty : accion);
                            //string c = string.Format(@"<li> {0} </li>", item.Nombre);
                            //output += c;

                            if (item.Accion1!=null) //La Propiedad "Accion1" Navega la relacion "Menu => Accion"
                            {
                                //    < ul >
                                //        < li > Digitalizacion de documentos </ li >  
                                //        < li > Profesionales y Matriculas </ li >    
                                //    </ ul >
                                string c = string.Format(@"<li id=""{2}"" data-jstree='{{""icon"":""{1}""}}'> {0}  <ul>", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o" : item.Icono, "menu_" + item.ID);

                                foreach (var Accion in item.Accion1)
                                {
                                    //c+= string.Format(@"<li > {0} </li>", Accion.Nombre);

                                    c += string.Format(@"<li id=""{2}"" data-jstree='{{""icon"":""{1}""}}'> {0} </li>", Accion.Nombre, string.IsNullOrEmpty(Accion.Icono) ? "fa fa-circle-o" : Accion.Icono, "accion_" + item.ID + "_" + Accion.ID);
                                }


                                c += "</ul></li>";
                                output += c;


                            }
                            else
                            {
                                string c = string.Format(@"<li id=""{2}"" data-jstree='{{""icon"":""{1}""}}'> {0} </li>", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o" : item.Icono, "menu_" + item.ID);
                                output += c;
                            }
                        }
                    }
                    //return output;
                }
                else
                {

                    foreach (var item in menusPadres)
                    {
                        string x = string.Format(@"<li id=""{2}"" data-jstree='{{""icon"":""{1}""}}'> {0} ", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o" : item.Icono, "menu_"+ item.ID);
                        output += x;

                        if (menusAll.Where(r => r.PadreID == item.ID).Any())
                        {
                            string y = @"<ul >";
                            output += y;

                            output = createTreeMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

                            string z = @"</ul>";
                            output += z;
                        }
                        output += "</li>";

                    }

                }
                return output;
            }
            else
            {
                return @"<li><a href=""#""><i class=""fa fa-circle-o text-red""></i> <span>No hay menus disponibles</span></a></li>";
            }
        }
        #endregion

    }
}