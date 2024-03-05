using AutoMapper;
using Driver.Api.Commands;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchDog;
using CreateDriverInfoRequest = Driver.Api.Commands.CreateDriverInfoRequest;

namespace Driver.Api.Controllers
{

    public class DriverController(IUnitOfWork unit, IMapper mapper, IMediator mediator) : BaseController(unit, mapper,mediator)
    {

        [HttpGet("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var driver = new GetDriverByIdQuery(driverId);
            var result=await _mediator.Send(driver);
            if(result is null)
                return NotFound("The Driver Can Not Found");
            WatchLogger.Log("Get Driver Function is Success ");

            return Ok(result);
        }
        [HttpPost("AddDriver")]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest DriverRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 
            var command = new CreateDriverInfoRequest(DriverRequest);
          var result= await _mediator.Send(command);
          WatchLogger.Log("Add Driver Function is Success ");

            return CreatedAtAction(nameof(GetDriver), new { driverId = result.DriverId }, result);
        }   
        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var query = new GetAllDriverQuery();
            var result =await _mediator.Send(query);
            WatchLogger.Log("Get All Driver Function is Success ");

            return Ok(result);
        }
        [HttpPut("UpdateDriver")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest DrivertRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var command = new UpdateDriverInfoRequest(DrivertRequest);
            var result = await _mediator.Send(command);
            return result ? NoContent() : BadRequest();
        }
        [HttpDelete("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
         
            var command = new DeleteDriverInfoRequest(driverId);
            var result = await _mediator.Send(command);
            WatchLogger.Log("Delete Driver Function is Success ");
            return result ? NoContent() : BadRequest();
        }

    }
}
