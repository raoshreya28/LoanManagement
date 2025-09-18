using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public class LoanOfficerRepository : ILoanOfficerRepository
    {
        private readonly AppDbContext _context;
        public LoanOfficerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync()
        {
            return await _context.LoanOfficers.ToListAsync();
        }

        public async Task<LoanOfficer?> GetByIdAsync(int id)
        {
            return await _context.LoanOfficers.FirstOrDefaultAsync(lo => lo.UserId == id);
        }

        public async Task<LoanOfficer> CreateAsync(LoanOfficer officer)
        {
            await _context.LoanOfficers.AddAsync(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer> EditAsync(LoanOfficer officer)
        {
            _context.LoanOfficers.Update(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.LoanOfficers.FirstOrDefaultAsync(lo => lo.UserId == id);
            if (existing != null)
            {
                _context.LoanOfficers.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
