using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class LoanAdminRepository : ILoanAdminRepository
    {
        private readonly AppDbContext _context;
        public LoanAdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanAdmin>> GetAllAsync()
        {
            return await _context.LoanAdmins.ToListAsync();
        }

        public async Task<LoanAdmin?> GetByIdAsync(int id)
        {
            return await _context.LoanAdmins.FirstOrDefaultAsync(a => a.AdminId == id);
        }

        public async Task<LoanAdmin> CreateAsync(LoanAdmin admin)
        {
            await _context.LoanAdmins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<LoanAdmin> EditAsync(LoanAdmin admin)
        {
            _context.LoanAdmins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.LoanAdmins.FirstOrDefaultAsync(a => a.AdminId == id);
            if (existing != null)
            {
                _context.LoanAdmins.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
