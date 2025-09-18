using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class RepaymentService : IRepaymentService
    {
        private readonly AppDbContext _context;

        public RepaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Repayment> CreateAsync(Repayment repayment)
        {
            await _context.Repayments.AddAsync(repayment);
            await _context.SaveChangesAsync();
            return repayment;
        }

        public async Task<Repayment> UpdateAsync(Repayment repayment)
        {
            _context.Repayments.Update(repayment);
            await _context.SaveChangesAsync();
            return repayment;
        }

        public async Task<IEnumerable<Repayment>> GetAllAsync()
        {
            return await _context.Repayments
                                 .Include(r => r.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<Repayment?> GetByIdAsync(int repaymentId)
        {
            return await _context.Repayments
                                 .Include(r => r.LoanApplication)
                                 .FirstOrDefaultAsync(r => r.RepaymentId == repaymentId);
        }

        public async Task<IEnumerable<Repayment>> GetRepaymentsByLoanAsync(int loanApplicationId)
        {
            return await _context.Repayments
                                 .Where(r => r.LoanApplicationId == loanApplicationId)
                                 .ToListAsync();
        }

        public async Task MarkAsPaidAsync(int repaymentId)
        {
            var repayment = await _context.Repayments.FindAsync(repaymentId);
            if (repayment != null)
            {
                repayment.Status = RepaymentStatus.PAID;
                repayment.PaidDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Repayment>> GetOverdueRepaymentsAsync()
        {
            var today = DateTime.UtcNow;
            return await _context.Repayments
                                 .Where(r => r.Status == RepaymentStatus.PENDING && r.DueDate < today)
                                 .ToListAsync();
        }
    }
}
