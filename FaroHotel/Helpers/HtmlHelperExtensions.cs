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
                            string a = string.Format(@"<li><a href=""#""><i class=""{1}""></i> {0} <span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a><ul class=""treeview-menu"">", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o text-aqua": item.Icono);
                            output += a;

                            output = createMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

                            string b = @"</ul></li>";
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
                            string c = string.Format(@"<li><a href=""{1}""><i class=""{2}""></i> {0} </a></li>", item.Nombre, url, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o text-aqua": item.Icono);
                            output += c;
                        }
                    }
                    //return output;
                }
                else
                {

                    foreach (var item in menusPadres)
                    {
                        string x = string.Format(@"<li class=""treeview""><a href = ""#"" ><i class=""{1}""></i><span> {0} </span><span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a>", item.Nombre, string.IsNullOrEmpty(item.Icono) ? "fa fa-circle-o": item.Icono);
                        output += x;

                        if (menusAll.Where(r => r.PadreID == item.ID).Any())
                        {
                            string y = @"<ul class=""treeview-menu"">";
                            output += y;

                            output = createMenu(menusAll.Where(r => r.PadreID == item.ID).ToList(), menusAll, output);

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




    }
}