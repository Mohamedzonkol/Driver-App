using AutoMapper;
using Driver.Api.Commands;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class GetDriverInfoHandler(IMapper mapper, IUnitOfWork unit)
        : IRequestHandler<CreateDriverInfoRequest,DriverResponses>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unit = unit;
    
        public async Task<DriverResponses> Handle(CreateDriverInfoRequest request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Drivers>(request.DriverRequest);
            await _unit.DriversRepo.Add(result);
            await _unit.CompleteAsync();
            return mapper.Map<DriverResponses>(result);
        }
    }
}
