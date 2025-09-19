using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly AppDbContext _context;
        public LoanApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllAsync()
        {
            return await _context.LoanApplications
                                 .Include(la => la.Customer)
                                 .Include(la => la.LoanScheme)
                                 .Include(la => la.LoanOfficer)
                                 .ToListAsync();
        }

        public async Task<LoanApplication?> GetByIdAsync(int id)
        {
            return await _context.LoanApplications
                                 .Include(la => la.Customer)
                                 .Include(la => la.LoanScheme)
                                 .Include(la => la.LoanOfficer)
                                 .FirstOrDefaultAsync(la => la.LoanApplicationId == id);
        }

        public async Task<LoanApplication> CreateAsync(LoanApplication application)
        {
            await _context.LoanApplications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<LoanApplication> EditAsync(LoanApplication application)
        {
            _context.LoanApplications.Update(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.LoanApplications.FirstOrDefaultAsync(la => la.LoanApplicationId == id);
            if (existing != null)
            {
                _context.LoanApplications.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}