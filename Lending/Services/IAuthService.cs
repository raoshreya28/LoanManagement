// ✅ IAuthService.cs
using Lending.Models;

namespace Lending.Services
{
    public interface IAuthService
    {
        LoginResponseViewModel Login(LoginViewModel login); // Sync is fine here
    }
}
