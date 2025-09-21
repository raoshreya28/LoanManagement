using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context)
        {
            _context = context;
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

        public async Task DeleteAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                                 .Include(l => l.Repayments)
                                 .Include(l => l.LoanApplication)
                                     .ThenInclude(la => la.Customer)
                                 .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _context.Loans
                                 .Include(l => l.Repayments)
                                 .Include(l => l.LoanApplication)
                                     .ThenInclude(la => la.Customer)
                                 .FirstOrDefaultAsync(l => l.LoanId == loanId);
        }
    }
}
