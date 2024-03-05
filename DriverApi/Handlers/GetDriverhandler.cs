using AutoMapper;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class GetDriverhandler(IMapper mapper, IUnitOfWork unit)
        : IRequestHandler<GetDriverByIdQuery, DriverResponses>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unit = unit;

        public async Task<DriverResponses?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var Driver = await _unit.DriversRepo.GetById(request.DriverId);
            return Driver == null ? null : mapper.Map<DriverResponses>(Driver);
        }
    }
}
