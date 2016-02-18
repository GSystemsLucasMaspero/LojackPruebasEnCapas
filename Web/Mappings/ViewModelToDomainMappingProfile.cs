using AutoMapper;
using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<EquipoFormViewModel, Equipo>()
                .ForMember(e => e.identificador, map => map.MapFrom(vm => vm.identificador))
                .ForMember(e => e.nroSerie, map => map.MapFrom(vm => vm.nroSerie))
                .ForMember(e => e.primario, map => map.MapFrom(vm => vm.primario))
                .ForMember(e => e.idEquipoTipo, map => map.MapFrom(vm => vm.idEquipoTipo))
                .ForMember(e => e.cadencia, map => map.MapFrom(vm => vm.cadencia))
                .ForMember(e => e.versionFirmware, map => map.MapFrom(vm => vm.versionFirmware))
                .ForMember(e => e.versionProgramacion, map => map.MapFrom(vm => vm.versionProgramacion))
                .ForMember(e => e.estadoSd, map => map.MapFrom(vm => vm.estadoSd))
                .ForMember(e => e.idCuenta, map => map.MapFrom(vm => vm.idCuenta))
                .ForMember(e => e.portable, map => map.MapFrom(vm => vm.portable));
        }
    }
}