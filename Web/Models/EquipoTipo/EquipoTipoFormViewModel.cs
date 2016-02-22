using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.EquipoTipo
{
    public class EquipoTipoFormViewModel
    {
        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Descripción no puede exceder los 50 caracteres.")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El campo Cantidad de Sensores es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Cantidad de Sensores no puede ser negativo.")]
        public int cantSensores { get; set; }
    }
}