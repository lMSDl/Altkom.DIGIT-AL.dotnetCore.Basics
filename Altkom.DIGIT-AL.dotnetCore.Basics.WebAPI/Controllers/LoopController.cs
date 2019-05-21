using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class LoopController : BaseController
    {
        public LoopController(ILogger<LoopController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customer = new LoopCustomer(){FirstName = "Adam"};
            customer.Customer = customer;

            return Ok(customer);
        }
    }

    public class LoopCustomer : Customer {
        public Customer Customer {get; set;}
    }
}