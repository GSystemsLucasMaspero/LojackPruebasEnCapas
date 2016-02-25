using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            #pragma warning disable 612, 618
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
            #pragma warning restore 612, 618
        }
    }
}