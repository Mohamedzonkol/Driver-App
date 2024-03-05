using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Queries
{
    public class GetDriverByIdQuery(Guid driverId) : IRequest<DriverResponses>
    {
        public Guid DriverId { get;  } = driverId;
    }
}
