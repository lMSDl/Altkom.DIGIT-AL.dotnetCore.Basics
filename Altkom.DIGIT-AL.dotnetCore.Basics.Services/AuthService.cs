using System;
using System.Net.Http;
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Services
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(HttpClient client) : base(client)
        {
        }

        protected override string Path => "api/auth";

        public async Task<string> Authenticate(User user)
        {
            try {
                 var response = await Client.PostAsJsonAsync(Path, user);
                 response.EnsureSuccessStatusCode();

                 return await response.Content.ReadAsStringAsync();
            }
            catch {
                return null;
            }
        }
    }
}
