﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Lojack_PruebaEntities : DbContext
    {
        public Lojack_PruebaEntities()
            : base("name=Lojack_PruebaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Entidad> Entidads { get; set; }
        public virtual DbSet<Equipo> Equipoes { get; set; }
        public virtual DbSet<EquipoTipo> EquipoTipoes { get; set; }
        public virtual DbSet<NivelServicio> NivelServicios { get; set; }
        public virtual DbSet<Posicion> Posicions { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
