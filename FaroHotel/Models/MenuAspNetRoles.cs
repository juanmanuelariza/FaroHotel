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
    
    public partial class MenuAspNetRoles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuAspNetRoles()
        {
            this.MenuAspNetRolesAccion = new HashSet<MenuAspNetRolesAccion>();
        }
    
        public int ID { get; set; }
        public int MenuId { get; set; }
        public string AspNetRolesId { get; set; }
    
        public virtual AspNetRoles AspNetRoles { get; set; }
        public virtual Menu Menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuAspNetRolesAccion> MenuAspNetRolesAccion { get; set; }
    }
}
