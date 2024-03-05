using AutoMapper;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Handlers
{
    public class GetAllAchevmentsHandler(IUnitOfWork unit, IMapper mapper)
        : IRequestHandler<GetAllAchevmentOuery, IEnumerable<DriverAchevmentResponse>>
    {
        public async Task<IEnumerable<DriverAchevmentResponse>> Handle(GetAllAchevmentOuery request, CancellationToken cancellationToken)
        {
            var achevments = await unit.AchevmentsRepo.GetAll();
            return mapper.Map<IEnumerable<DriverAchevmentResponse>>(achevments);
        }
    }
}
