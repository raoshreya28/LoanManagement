using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _context;

        public LoanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                                 .Include(l => l.Customer)
                                .Include(l => l.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _context.Loans
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanApplication)
                                 .FirstOrDefaultAsync(l => l.LoanId == loanId);
        }

        public async Task<IEnumerable<Loan>> GetLoansByCustomerAsync(int customerId)
        {
            return await _context.Loans
                                 .Where(l => l.CustomerId == customerId)
                                 .Include(l => l.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetNPALoansAsync()
        {
            var today = DateTime.UtcNow;
            return await _context.Loans
                                 .Include(l => l.Repayments)
                                 .Where(l => l.Repayments.Any(r => r.Status == RepaymentStatus.OVERDUE))
                                 .Include(l => l.Customer)
                                 .ToListAsync();
        }
    }
}
