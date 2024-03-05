using AutoMapper;
using Driver.Api.Commands;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class DeleteDriverInfoHandler(IMapper mapper, IUnitOfWork unit)
        : IRequestHandler<DeleteDriverInfoRequest, bool>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unit = unit;

        public async Task<bool> Handle(DeleteDriverInfoRequest request, CancellationToken cancellationToken)
        {
            var Driver = await _unit.DriversRepo.GetById(request.DriverId);
            if (Driver is null)
                return false;
            await _unit.DriversRepo.Delete(request.DriverId);
            await _unit.CompleteAsync();
            return true;
        }
    }
}
