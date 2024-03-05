using AutoMapper;
using Driver.Api.Commands;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class UpdateDriverInfohandler(IMapper mapper, IUnitOfWork unit)
        : IRequestHandler<UpdateDriverInfoRequest, bool>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unit = unit;

        public async Task<bool> Handle(UpdateDriverInfoRequest request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Drivers>(request.DriverRequest);
            await _unit.DriversRepo.Update(result);
            await _unit.CompleteAsync();
            return true;
        }
    }
}
