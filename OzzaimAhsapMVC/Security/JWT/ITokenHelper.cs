

using OzzaimAhsapMVC.Models;

namespace OzzaimAhsapMVC.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
