using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.IServices
{
    public interface IAuthService
    {
        Task<string> Authenticate(User user);
    }
}