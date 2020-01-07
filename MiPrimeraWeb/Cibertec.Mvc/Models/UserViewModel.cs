using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cibertec.Mvc.Models
{
    public class UserViewModel
    {
        [Required] //Es obligatoria y si no lo tiene te muestra un msje de error
        [DataType(DataType.EmailAddress)]//Acepta cadenas que tenga la cadena email
        public string Email { get; set; }

        [Required] //Es obligatoria y si no lo tiene te muestra un msje de error
        [DataType(DataType.Password)]//Para que valide las carectiristicas que debe tener una contraseña
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}