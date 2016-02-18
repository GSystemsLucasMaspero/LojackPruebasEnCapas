using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EquipoFormViewModel
    {
        [Required(ErrorMessage = "El campo Identificador es obligatorio.")]
        [StringLength(15, ErrorMessage = "El campo Identificador no puede exceder los 15 caracteres.")]
        public string identificador { get; set; }
        [Required(ErrorMessage = "El campo Número de Serie es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo Número de Serie no puede exceder los 20 caracteres.")]
        public string nroSerie { get; set; }
        public bool primario { get; set; }
        [Required(ErrorMessage = "El campo Tipo de Equipo es obligatorio.")]
        public int? idEquipoTipo { get; set; }
        [Required(ErrorMessage = "El campo Cadencia es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Cadencia no puede ser negativo.")]
        public int cadencia { get; set; }
        [StringLength(50, ErrorMessage = "El campo Versión de Firmware no puede exceder los 50 caracteres.")]
        public string versionFirmware { get; set; }
        [StringLength(50, ErrorMessage = "El campo Versión de Programación no puede exceder los 50 caracteres.")]
        public string versionProgramacion { get; set; }
        [StringLength(50, ErrorMessage = "El campo Estado SD no puede exceder los 50 caracteres.")]
        public string estadoSd { get; set; }
        public Nullable<int> idCuenta { get; set; }
        public bool portable { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        public virtual EquipoTipo EquipoTipo { get; set; }
    }
}