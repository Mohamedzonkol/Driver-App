using AutoMapper;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;

namespace Driver.Api.MappingConfigration
{
    public class DominToResponse:Profile
    {
        public DominToResponse()
        {
            CreateMap<Achevment, DriverAchevmentResponse>();
            CreateMap<Drivers, DriverResponses>().ForMember(o => o.DriverId,
                    s => s.MapFrom(s =>
                        s.Id))
                .ForMember(o => o.FullName,
                s => s.MapFrom(s =>
                   $"{s.FirstName}  {s.LastName}"));
        }
    }
}
