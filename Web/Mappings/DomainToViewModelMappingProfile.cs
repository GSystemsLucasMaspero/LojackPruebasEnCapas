﻿using AutoMapper;
using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
using Web.Models.Entidad;
using Web.Models.EquipoTipo;
using Web.Models.NivelServicio;
using Web.Models.Posicion;

namespace Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            #pragma warning disable 612, 618
            Mapper.CreateMap<Equipo, EquipoViewModel>();
            Mapper.CreateMap<Equipo, EquipoFormViewModel>();

            Mapper.CreateMap<Entidad, EntidadViewModel>();
            Mapper.CreateMap<Entidad, EntidadFormViewModel>();

            Mapper.CreateMap<EquipoTipo, EquipoTipoViewModel>();
            Mapper.CreateMap<EquipoTipo, EquipoTipoFormViewModel>();

            Mapper.CreateMap<NivelServicio, NivelServicioViewModel>();
            Mapper.CreateMap<NivelServicio, NivelServicioFormViewModel>();

            Mapper.CreateMap<Posicion, PosicionViewModel>();
            #pragma warning restore 612, 618
        }
    }
}