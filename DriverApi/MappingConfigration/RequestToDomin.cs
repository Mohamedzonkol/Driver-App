using AutoMapper;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Requests;

namespace Driver.Api.MappingConfigration
{
    public class RequestToDomin:Profile
    {
        public RequestToDomin()
        {

            CreateMap<CreateDriverAchevmentRequest, Achevment>().
                ForMember(s => s.Status, o => o.MapFrom(s => 1))
                .ForMember(s => s.CreatedAt, o => o.MapFrom(m => DateTime.Now))
                .ForMember(s => s.UpdateDateTime, o => o.MapFrom(m => DateTime.Now));


            CreateMap<UpdateDriverAchevmentRequest, Achevment>().
                ForMember(s => s.UpdateDateTime, o => o.MapFrom(m => DateTime.Now));

            CreateMap<CreateDriverRequest, Drivers>().
                ForMember(s => s.Status, o => o.MapFrom(s => 1))
                .ForMember(s => s.CreatedAt, o => o.MapFrom(m => DateTime.Now))
                .ForMember(s => s.UpdateDateTime, o => o.MapFrom(m => DateTime.Now));


            CreateMap<UpdateDriverRequest, Drivers>().
                ForMember(s => s.UpdateDateTime, o => o.MapFrom(m => DateTime.Now));


        }
    }
}
