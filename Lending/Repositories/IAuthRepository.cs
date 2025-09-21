using Lending.Models;

namespace Lending.Repositories
{
    public interface IAuthRepository
    {
        LoginResponseViewModel Login(LoginViewModel login);
    }
}
