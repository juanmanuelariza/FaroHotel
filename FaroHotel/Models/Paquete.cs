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
    
    public partial class Paquete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paquete()
        {
            this.Ventanilla = new HashSet<Ventanilla>();
        }
    
        public int ID { get; set; }
        public string Titulo { get; set; }
        public byte DescripcionId { get; set; }
        public byte TemporadaId { get; set; }
        public byte NochesId { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public double PrecioSingle { get; set; }
        public double PrecioDoble { get; set; }
        public byte CuotasId { get; set; }
        public System.DateTime FechaAlta { get; set; }
    
        public virtual TipoCuota TipoCuota { get; set; }
        public virtual TipoDescripcionPaquete TipoDescripcionPaquete { get; set; }
        public virtual TipoNoche TipoNoche { get; set; }
        public virtual TipoTemporada TipoTemporada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ventanilla> Ventanilla { get; set; }
    }
}
