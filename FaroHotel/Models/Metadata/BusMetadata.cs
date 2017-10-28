using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    [MetadataType(typeof(BusMetadata))]
    public partial class Bus
    {
    }
    public class BusMetadata
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Fecha { get; set; }
    }
}