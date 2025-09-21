using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class AuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public LoginResponseViewModel Login(LoginViewModel login)
        {
            var user = _context.Users
                .Include(u => u.Role)   // IMPORTANT
                .FirstOrDefault(u => u.UserName == login.UserName && u.PasswordHash == login.Password);

            if (user == null)
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

