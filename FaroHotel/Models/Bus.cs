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
    
    public partial class Bus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bus()
        {
            this.EnlaceBusPasajero = new HashSet<EnlaceBusPasajero>();
        }
    
        public int ID { get; set; }
        public System.DateTime Fecha { get; set; }
        public int TrayectoId { get; set; }
    
        public virtual TipoTrayecto TipoTrayecto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnlaceBusPasajero> EnlaceBusPasajero { get; set; }
    }
}
