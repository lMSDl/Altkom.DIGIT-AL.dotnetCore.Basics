using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.IServices
{
    public interface IAuthService
    {
        string Authenticate(User user);
    }
}