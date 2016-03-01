using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Usuario
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}