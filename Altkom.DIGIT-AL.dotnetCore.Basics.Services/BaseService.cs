using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Services
{
    public abstract class BaseService
    {
        protected HttpClient Client {get;}

        abstract protected string Path {get;}

        public BaseService(HttpClient client) {
            Client = client;
        }
    }
}
