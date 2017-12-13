using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    public partial class Paquete
    {
        public int[] VentanillaIds { get; set; }
    }

    public partial class Habitacion
    {
        public int[] TipoHabitacionIds { get; set; }
    }
}