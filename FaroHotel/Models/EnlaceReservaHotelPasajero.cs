//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FaroHotel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnlaceReservaHotelPasajero
    {
        public int ReservaHotelId { get; set; }
        public int PasajeroId { get; set; }
        public int PaqueteId { get; set; }
        public byte BaseId { get; set; }
        public int ID { get; set; }
    
        public virtual Paquete Paquete { get; set; }
        public virtual Pasajero Pasajero { get; set; }
        public virtual ReservaHotel ReservaHotel { get; set; }
        public virtual TipoBase TipoBase { get; set; }
    }
}
