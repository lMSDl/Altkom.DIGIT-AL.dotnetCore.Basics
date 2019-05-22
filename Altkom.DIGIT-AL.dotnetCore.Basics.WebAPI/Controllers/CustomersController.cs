using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Microsoft.AspNetCore.Authorization;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomersService _customerService;
        public CustomersController(ILogger<CustomersController> logger, ICustomersService customerService) : base(logger)
        {
            _customerService = customerService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            Logger.LogDebug("GetCustomers");
            var customers = (await _customerService.GetAsync()).ToList();
            //customers.AddRange(await _customerService.GetAsync());
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Produces("application/xml")]
        public async Task<IActionResult> Get(int id)
        {
            Logger.LogDebug($"GetRequest {id}");
            if(id == 5)
                return NotFound();
            return Ok(await _customerService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            customer.Id = await _customerService.AddAsync(customer);
            if(customer.Id == 0)
                return Conflict();
            return CreatedAtAction(nameof(Get), new {id = customer.Id}, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if(id != customer.Id)
                return BadRequest();

            var localCustomer = await _customerService.GetAsync(id);
            if(localCustomer == null)
                return NotFound();
                
            if(!await _customerService.UpdateAsync(id, customer))
                return StatusCode(304);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetAsync(id);
            if(customer == null)
                return NotFound();

            if (!await _customerService.DeleteAsync(id))
                return StatusCode(304);

            return Ok();
        }
    }
}