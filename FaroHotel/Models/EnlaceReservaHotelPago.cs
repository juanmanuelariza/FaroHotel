//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FaroHotel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnlaceReservaHotelPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnlaceReservaHotelPago()
        {
            this.EnlaceReservaHotelDescuento = new HashSet<EnlaceReservaHotelDescuento>();
        }
    
        public int ID { get; set; }
        public int ReservaHotelId { get; set; }
        public int TipoFormaDePagoId { get; set; }
        public double Monto { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<int> NroComprobante { get; set; }
        public string NombreTitular { get; set; }
        public Nullable<int> DniTitular { get; set; }
    
        public virtual ReservaHotel ReservaHotel { get; set; }
        public virtual TipoFormaDePago TipoFormaDePago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnlaceReservaHotelDescuento> EnlaceReservaHotelDescuento { get; set; }
    }
}
