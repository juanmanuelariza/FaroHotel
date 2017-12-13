using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    [MetadataType(typeof(PasajeroMetadata))]
    public partial class Pasajero
    {
    }
    public class PasajeroMetadata
    {
        [Required(ErrorMessage = "El campo DNI es obligatorio.")]
        public long DNI { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo sexo es obligatorio.")]
        public byte Sexo { get; set; }
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaNacimiento { get; set; }
    }
}