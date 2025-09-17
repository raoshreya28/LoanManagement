using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanApplication)
                                 .Include(l => l.Repayments)
                                 .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanApplication)
                                 .Include(l => l.Repayments)
                                 .FirstOrDefaultAsync(l => l.LoanId == id);
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> EditAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == id);
            if (existing != null)
            {
                _context.Loans.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
