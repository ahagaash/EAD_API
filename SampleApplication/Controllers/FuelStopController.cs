using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using SampleApplication.Models;
using SampleApplication.Services;
using ZstdSharp.Unsafe;

namespace SampleApplication.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class FuelStopController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        

        public FuelStopController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<FuelStop>> GetAllFuelStops()
        {
            return await _mongoDBService.GetALLFuelStopsAsync();
        }

        [HttpGet]
        public async Task<List<FuelStop>> GetAllFuelStopsbyID()
        {
            return await _mongoDBService.GetALLFuelStopsAsync();
        }

        [HttpGet("{id}")]
        public async Task <FuelStop> GetFuelStopbyID( string id )
        {
          return  await _mongoDBService.GetFuelStopbyIdAsync( id );
        }


        [HttpPost]
        public async Task<IActionResult> AddFuelStop([FromBody] FuelStop fuelStop)
        {
            await _mongoDBService.CreateAsycAddFuel(fuelStop);
            return CreatedAtAction(nameof(GetAllFuelStops), new { id = fuelStop.Id }, fuelStop);
        }

        [HttpPatch("updateQueue/{id}")]
        public async Task<IActionResult> AddToDiselQueue(string id,  [FromBody] string vechiletype)
        {
            await _mongoDBService.AddToFuelQueueAsync(id, vechiletype);
            return NoContent();
        }


        [HttpPatch("updateQueuedecrement/{id}")]
        public async Task<IActionResult> decreaseQueue(string id, [FromBody] string vechiletype)
        {
            await _mongoDBService.decreaseToFuelQueueAsync(id, vechiletype);
            return NoContent();
        }

        [HttpPatch("decreasepetrolfuelquantity/{id}")]
        public async Task<IActionResult> updatepetrolQunatity(string id, [FromBody] string fueltype,  double fuelQuantity)
        {
            await _mongoDBService.decreaseToFuelQueueAsync(id, fueltype, fuelQuantity);
            return NoContent();
        }



    }
}
