using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FaroHotel.Models
{
    public class UsuarioVM
    {
        public string Id { get; set; }
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Nullable<int> VentanillaId { get; set; }
    }
}