using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customerService;
        public CustomersController(ICustomersService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetAsync(id);
            if(customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Customer customer)
        {
            if(await _customerService.DeleteAsync(customer.Id))
                return RedirectToAction(nameof(Index));
            return View(customer);
        }

        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public async Task<IActionResult> Create(/*[Bind("FirstName,LastName,Gender")]*/Customer customer)
        {
            if(ModelState.IsValid)
            {
                await _customerService.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetAsync(id);
            if(customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,FirstName,LastName,Gender,LoyalityCard")]*/Customer customer)
        {
            if(ModelState.IsValid)
            {
                await _customerService.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

    }
}