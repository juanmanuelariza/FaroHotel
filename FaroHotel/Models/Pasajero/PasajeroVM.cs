using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    public class PasajeroVM
    {
        public int ID { get; set; }
        public long DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public byte Sexo { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public Nullable<long> Telefono { get; set; }
        public string Email { get; set; }
        public bool Diabetes { get; set; }
        public bool Celiaquia { get; set; }
        public bool Motricidad { get; set; }
        public bool ListaNegra { get; set; }

        public virtual TipoSexo TipoSexo { get; set; }
    }
}