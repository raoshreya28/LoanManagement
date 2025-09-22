using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public LoginResponseViewModel Login(LoginViewModel login)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == login.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return new LoginResponseViewModel { IsSuccess = false };
            }

            return new LoginResponseViewModel
            {
                IsSuccess = true,
                User = user
            };
        }



    }
}

