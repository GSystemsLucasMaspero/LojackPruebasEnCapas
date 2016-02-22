using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Entidad
{
    public class EntidadFormViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El campo Nombre no puede exceder los 30 caracteres.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Estado no puede ser negativo.")]
        public int estado { get; set; }
        [Required(ErrorMessage = "El campo Nivel Servicio es obligatorio.")]
        public int? idNivelServicio { get; set; }
        [StringLength(50, ErrorMessage = "El campo Plantilla Suceso no puede exceder los 50 caracteres.")]
        public string plantillaSuceso { get; set; }
        [Required(ErrorMessage = "El campo Cadencia Reporte es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "Cadencia Reporte no puede ser negativo.")]
        public int cadenciaReporte { get; set; }
        [StringLength(40, ErrorMessage = "El campo Comentario no puede exceder los 40 caracteres.")]
        public string comentario { get; set; }
        [StringLength(255, ErrorMessage = "El campo Teléfono no puede exceder los 255 caracteres.")]
        public string telefono { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "ID Procedimiento no puede ser negativo.")]
        public Nullable<int> idProcedimiento { get; set; }
        public Nullable<int> idCuenta { get; set; }

        public virtual Cuenta Cuenta { get; set; }
        public virtual NivelServicio NivelServicio { get; set; }
    }
}