using AutoMapper;
using Datos.DBContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;

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
                .ForMember(e => e.identificador, map => map.MapFrom(vm => vm.EquipoIdentificador))
                .ForMember(e => e.nroSerie, map => map.MapFrom(vm => vm.EquipoNroSerie));
        }
    }
}