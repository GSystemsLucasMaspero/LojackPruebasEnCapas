using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.NivelServicio
{
    public class NivelServicioFormViewModel
    {
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Descripción no puede exceder los 20 caracteres.")]
        public string descripcion { get; set; }
    }
}