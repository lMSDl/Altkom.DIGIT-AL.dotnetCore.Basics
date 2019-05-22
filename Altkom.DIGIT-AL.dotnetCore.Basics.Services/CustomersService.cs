using System;
using System.Net.Http;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Services
{
    public class CustomersService : BaseEntityService<Customer>, ICustomersService
    {
        public CustomersService(HttpClient client) : base(client)
        {
        }

        protected override string Path => "api/customers";
    }
}
