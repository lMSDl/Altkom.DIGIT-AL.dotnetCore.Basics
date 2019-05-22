using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebApp.Pages.Customers
{
    public abstract class BasePageModel : PageModel {
        
        protected ICustomersService CustomerService {get;}
        public BasePageModel(ICustomersService customerService)
        {
            CustomerService = customerService;
        }
    }
}