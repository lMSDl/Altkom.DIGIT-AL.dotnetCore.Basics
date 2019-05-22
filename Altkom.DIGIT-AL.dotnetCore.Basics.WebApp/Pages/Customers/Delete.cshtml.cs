using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebApp.Pages.Customers
{
    public class DeleteCustomer : BasePageModel
    {
        public DeleteCustomer(ICustomersService customersService) : base(customersService)
        {
        }
        
        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await CustomerService.GetAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await CustomerService.DeleteAsync(id);

            return RedirectToPage("./Index");
        }
    }
}