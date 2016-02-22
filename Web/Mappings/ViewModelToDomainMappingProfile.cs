using AutoMapper;
using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
using Web.Models.Entidad;
using Web.Models.EquipoTipo;
using Web.Models.NivelServicio;

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

            Mapper.CreateMap<EntidadFormViewModel, Entidad>()
                .ForMember(e => e.nombre, map => map.MapFrom(vm => vm.nombre))
                .ForMember(e => e.estado, map => map.MapFrom(vm => vm.estado))
                .ForMember(e => e.idNivelServicio, map => map.MapFrom(vm => vm.idNivelServicio))
                .ForMember(e => e.plantillaSuceso, map => map.MapFrom(vm => vm.plantillaSuceso))
                .ForMember(e => e.cadenciaReporte, map => map.MapFrom(vm => vm.cadenciaReporte))
                .ForMember(e => e.comentario, map => map.MapFrom(vm => vm.comentario))
                .ForMember(e => e.telefono, map => map.MapFrom(vm => vm.telefono))
                .ForMember(e => e.idCuenta, map => map.MapFrom(vm => vm.idCuenta))
                .ForMember(e => e.idProcedimiento, map => map.MapFrom(vm => vm.idProcedimiento));

            Mapper.CreateMap<EquipoTipoFormViewModel, EquipoTipo>()
                .ForMember(e => e.descripcion, map => map.MapFrom(vm => vm.descripcion))
                .ForMember(e => e.cantSensores, map => map.MapFrom(vm => vm.cantSensores));

            Mapper.CreateMap<NivelServicioFormViewModel, NivelServicio>()
                .ForMember(e => e.descripcion, map => map.MapFrom(vm => vm.descripcion));
        }
    }
}