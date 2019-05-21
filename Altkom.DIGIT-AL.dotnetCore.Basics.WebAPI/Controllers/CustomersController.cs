using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(ILogger<CustomersController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            Logger.LogDebug("GetCustomers");
            return Ok(new Customer[] { new Customer{FirstName = "Adam", LastName="Adamski", Id = 1}, new Customer{Id = 2, FirstName="Piotr", LastName="Piotrkowski"} });
        }
    }
}