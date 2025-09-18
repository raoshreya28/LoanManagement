using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanAdminService : ILoanAdminService
    {
        private readonly AppDbContext _context;

        public LoanAdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAdmin> CreateAsync(LoanAdmin admin)
        {
            await _context.LoanAdmins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<LoanAdmin> UpdateAsync(LoanAdmin admin)
        {
            _context.LoanAdmins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task DeleteAsync(int adminId)
        {
            var admin = await _context.LoanAdmins.FindAsync(adminId);
            if (admin != null)
            {
                _context.LoanAdmins.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LoanAdmin>> GetAllAsync()
        {
            return await _context.LoanAdmins.ToListAsync();
        }

        public async Task<LoanAdmin?> GetByIdAsync(int adminId)
        {
            return await _context.LoanAdmins.FindAsync(adminId);
        }
    }
}
