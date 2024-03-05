using AutoMapper;
using Driver.Api.Queries;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Driver.Entites.DTOs.Requests;
using Driver.Entites.DTOs.Responeses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static StackExchange.Redis.Role;

namespace Driver.Api.Controllers
{

    public class AchevmentController : BaseController
    {
        public AchevmentController(IUnitOfWork unit, IMapper mapper, IMediator mediator) : base(unit, mapper,mediator)
        {
        }

        [HttpGet("{driverId:Guid}")]
        public async Task<IActionResult> GetDriverAchievement(Guid driverId)
        {
         var DriverAchevment  = await _unit.AchevmentsRepo.GetDriverAchevments(driverId);
         if (DriverAchevment == null) 
             return NotFound("Oops Driver Not Found !");
        var result= _mapper.Map<DriverAchevmentResponse>(DriverAchevment);
        
        return Ok(result);

        }
        [HttpPost("")]
        public async Task<IActionResult> AddAchievement([FromBody]CreateDriverAchevmentRequest achevmentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           var result= _mapper.Map<Achevment>(achevmentRequest);
           await _unit.AchevmentsRepo.Add(result);
           await _unit.CompleteAsync();
           return CreatedAtAction(nameof(GetDriverAchievement),new {driverId=result.DriverId},result);
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateAchievement([FromBody] UpdateDriverAchevmentRequest achevmentRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _mapper.Map<Achevment>(achevmentRequest);
            await _unit.AchevmentsRepo.Update(result);
            await _unit.CompleteAsync();
            return NoContent();
        }
        [HttpGet("GetAllAchevment")]
        public async Task<IActionResult> GetAllAchevment()
        {
            var query = new GetAllAchevmentOuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
