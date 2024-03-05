using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Commands
{
    public class UpdateDriverInfoRequest(UpdateDriverRequest driverRequest) : IRequest<bool>
    {
        public UpdateDriverRequest DriverRequest { get; } = driverRequest;
    }
}
