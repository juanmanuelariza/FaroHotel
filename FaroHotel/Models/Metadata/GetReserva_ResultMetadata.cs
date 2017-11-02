using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    [MetadataType(typeof(GetReserva_ResultMetadata))]
    public partial class GetReserva_Result
    {
    }
    public class GetReserva_ResultMetadata
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaEntrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaSalida { get; set; }
    }
}