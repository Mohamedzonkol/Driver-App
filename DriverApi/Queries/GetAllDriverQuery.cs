using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Responeses;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Driver.Api.Queries
{
    public class GetAllDriverQuery:IRequest<IEnumerable<DriverResponses>>
    {
    }
}
