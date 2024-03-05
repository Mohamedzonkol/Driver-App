using AutoMapper;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class GetAllDriverHandler(IUnitOfWork unit, IMapper mapper)
        : IRequestHandler<GetAllDriverQuery, IEnumerable<DriverResponses>>
    {
        public async Task<IEnumerable<DriverResponses>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await unit.DriversRepo.GetAll(); 
            return mapper.Map<IEnumerable<DriverResponses>>(driver);
        }
    }
}
