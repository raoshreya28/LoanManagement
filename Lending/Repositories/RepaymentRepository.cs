using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class RepaymentRepository : IRepaymentRepository
    {
        private readonly AppDbContext _context;
        public RepaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Repayment>> GetAllAsync()
        {
            return await _context.Repayments
                                 .Include(r => r.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<Repayment?> GetByIdAsync(int id)
        {
            return await _context.Repayments
                                 .Include(r => r.LoanApplication)
                                 .FirstOrDefaultAsync(r => r.RepaymentId == id);
        }

        public async Task<Repayment> CreateAsync(Repayment repayment)
        {
            await _context.Repayments.AddAsync(repayment);
            await _context.SaveChangesAsync();
            return repayment;
        }

        public async Task<Repayment> EditAsync(Repayment repayment)
        {
            _context.Repayments.Update(repayment);
            await _context.SaveChangesAsync();
            return repayment;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Repayments.FirstOrDefaultAsync(r => r.RepaymentId == id);
            if (existing != null)
            {
                _context.Repayments.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
