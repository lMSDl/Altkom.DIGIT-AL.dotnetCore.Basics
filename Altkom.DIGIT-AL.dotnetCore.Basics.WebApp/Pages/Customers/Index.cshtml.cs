using System.Collections.Generic;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebApp.Pages.Customers
{
    public class IndexCustomers : BasePageModel {
        public IndexCustomers(ICustomersService customerService) : base(customerService)
        {
        }
        public IList<Customer> Customers { get;set; }

        public async Task OnGetAsync()
        {
            Customers = (await CustomerService.GetAsync()).ToList();
        }
    }
}