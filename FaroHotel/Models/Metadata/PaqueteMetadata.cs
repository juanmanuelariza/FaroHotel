using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    [MetadataType(typeof(PaqueteMetadata))]
    public partial class Paquete
    {
    }
    public class PaqueteMetadata
    {
        [Required(ErrorMessage = "El campo titulo es obligatorio.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        public byte DescripcionId { get; set; }

        [Required(ErrorMessage = "El campo temporada es obligatorio.")]
        public byte TemporadaId { get; set; }

        [Required(ErrorMessage = "El campo noches es obligatorio.")]
        public byte NochesId { get; set; }

        [Required(ErrorMessage = "El campo fecha inicio es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo Fecha fin es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaFin { get; set; }


        [Required(ErrorMessage = "El campo Precio base single es obligatorio.")]
        public double PrecioSingle { get; set; }

        [Required(ErrorMessage = "El campo fecha base doble es obligatorio.")]
        public double PrecioDoble { get; set; }

        [Required(ErrorMessage = "El campo cuotas es obligatorio.")]
        public byte CuotasId { get; set; }
    }
}