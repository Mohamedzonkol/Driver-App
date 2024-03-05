using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Commands
{
    public class CreateDriverInfoRequest(CreateDriverRequest driverRequest) : IRequest<DriverResponses>
    {
        public CreateDriverRequest DriverRequest { get; } = driverRequest;
    }
}
