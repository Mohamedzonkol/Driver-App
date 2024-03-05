using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Commands
{
    public class DeleteDriverInfoRequest(Guid driverId) : IRequest<bool>
    {
        public Guid DriverId { get; } = driverId;
    }
}
