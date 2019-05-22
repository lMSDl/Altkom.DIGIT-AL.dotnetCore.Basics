using System.Collections.Generic;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebApp.Pages.Customers
{
    public class DeleteCustomer : BasePageModel {
        public DeleteCustomer(ICustomersService customerService) : base(customerService)
        {
        }

        public Customer Customer {get; set;}

        public async Task OnGetAsync(int id) {
            Customer = await CustomerService.GetAsync(id);
        }

    }
}