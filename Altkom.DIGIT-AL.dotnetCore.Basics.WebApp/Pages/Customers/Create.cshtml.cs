using System.Collections.Generic;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebApp.Pages.Customers
{
    public class CreateCustomer : BasePageModel {
        public CreateCustomer(ICustomersService customerService) : base(customerService)
        {
        }

        [BindProperty]
        public Customer Customer {get; set;}

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if(!ModelState.IsValid)
            return Page();

            await CustomerService.AddAsync(Customer);
            return RedirectToPage("./Index");
        }

    }
}