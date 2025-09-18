using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanSchemeService : ILoanSchemeService
    {
        private readonly AppDbContext _context;

        public LoanSchemeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanScheme> CreateAsync(LoanScheme scheme)
        {
            await _context.LoanSchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme> UpdateAsync(LoanScheme scheme)
        {
            _context.LoanSchemes.Update(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task DeleteAsync(int schemeId)
        {
            var scheme = await _context.LoanSchemes.FindAsync(schemeId);
            if (scheme != null)
            {
                _context.LoanSchemes.Remove(scheme);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LoanScheme>> GetAllAsync()
        {
            return await _context.LoanSchemes.Include(l => l.LoanApplications).ToListAsync();
        }

        public async Task<LoanScheme?> GetByIdAsync(int schemeId)
        {
            return await _context.LoanSchemes
                                 .Include(l => l.LoanApplications)
                                 .FirstOrDefaultAsync(l => l.LoanSchemeId == schemeId);
        }
    }
}
