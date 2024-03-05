using AutoMapper;
using Driver.DataServices.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController(IUnitOfWork unit, IMapper mapper, IMediator mediator) : ControllerBase
    {
        protected readonly IUnitOfWork _unit = unit;
        protected readonly IMapper _mapper = mapper;
        protected readonly IMediator _mediator = mediator;
    }

}
