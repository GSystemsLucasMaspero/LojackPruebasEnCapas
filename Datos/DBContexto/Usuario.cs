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
    
    public partial class Usuario
    {
        public int idUsuario { get; set; }
        public int idCliente { get; set; }
        public string userLogin { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idSector { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> lastExpiredPassword { get; set; }
        public bool operador { get; set; }
        public Nullable<bool> operadorSeguridad { get; set; }
        public bool supervisor { get; set; }
        public string email { get; set; }
        public System.DateTime fechaAlta { get; set; }
        public int usuarioAlta { get; set; }
        public System.DateTime fechaModificacion { get; set; }
        public int usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaBaja { get; set; }
        public Nullable<int> usuarioBaja { get; set; }
        public int nivelAuditoria { get; set; }
        public int perfilWindows { get; set; }
        public int perfilWeb { get; set; }
        public bool multipais { get; set; }
        public bool demo { get; set; }
        public int diasDemo { get; set; }
        public Nullable<int> idCuenta { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
