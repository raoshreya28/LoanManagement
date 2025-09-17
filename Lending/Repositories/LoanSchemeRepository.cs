using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class LoanSchemeRepository : ILoanSchemeRepository
    {
        private readonly AppDbContext _context;
        public LoanSchemeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanScheme>> GetAllAsync()
        {
            return await _context.LoanSchemes
                                 .Include(ls => ls.LoanApplications)
                                 .ToListAsync();
        }

        public async Task<LoanScheme?> GetByIdAsync(int id)
        {
            return await _context.LoanSchemes
                                 .Include(ls => ls.LoanApplications)
                                 .FirstOrDefaultAsync(ls => ls.LoanSchemeId == id);
        }

        public async Task<LoanScheme> CreateAsync(LoanScheme scheme)
        {
            await _context.LoanSchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task<LoanScheme> EditAsync(LoanScheme scheme)
        {
            _context.LoanSchemes.Update(scheme);
            await _context.SaveChangesAsync();
            return scheme;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.LoanSchemes.FirstOrDefaultAsync(ls => ls.LoanSchemeId == id);
            if (existing != null)
            {
                _context.LoanSchemes.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
