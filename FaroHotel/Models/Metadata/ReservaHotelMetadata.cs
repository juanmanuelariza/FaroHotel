using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{

    [MetadataType(typeof(ReservaHotelMetadata))]
    public partial class ReservaHotel
    {
    }
    public class ReservaHotelMetadata
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaEntrada { get; set; }
             
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaSalida { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }
    }
}