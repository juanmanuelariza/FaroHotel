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
    
    public partial class GetReserva_Result
    {
        public int ID { get; set; }
        public System.DateTime FechaEntrada { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public string Hotel { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public bool Confirmada { get; set; }
        public long DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<long> Telefono { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NombreVentanilla { get; set; }
    }
}