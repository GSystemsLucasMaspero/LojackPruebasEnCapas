//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos.DBContexto
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipo
    {
        public Equipo()
        {
            this.Posicions = new HashSet<Posicion>();
        }
    
        public int idEquipo { get; set; }
        public string identificador { get; set; }
        public string nroSerie { get; set; }
        public bool primario { get; set; }
        public int idEquipoTipo { get; set; }
        public int cadencia { get; set; }
        public System.DateTime fechaAlta { get; set; }
        public int usuarioAlta { get; set; }
        public string versionFirmware { get; set; }
        public string versionProgramacion { get; set; }
        public System.DateTime fechaModificacion { get; set; }
        public int usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaBaja { get; set; }
        public Nullable<int> usuarioBaja { get; set; }
        public string estadoSd { get; set; }
        public Nullable<int> idCuenta { get; set; }
        public bool portable { get; set; }
    
        public virtual Cuenta Cuenta { get; set; }
        public virtual EquipoTipo EquipoTipo { get; set; }
        public virtual ICollection<Posicion> Posicions { get; set; }
    }
}
