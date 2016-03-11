using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Usuario
{
    public class UsuarioRegister
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "Debes confirmar tu contraseña.")]
        [Compare("Password", ErrorMessage = "Las constraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}