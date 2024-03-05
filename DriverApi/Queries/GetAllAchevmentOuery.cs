using Driver.Entites.DTOs.Responeses;
using MediatR;

namespace Driver.Api.Queries
{
    public class GetAllAchevmentOuery:IRequest<IEnumerable<DriverAchevmentResponse>>
    {
    }
}
