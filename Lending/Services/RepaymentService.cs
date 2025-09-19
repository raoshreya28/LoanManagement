using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class RepaymentService : IRepaymentService
    {
        private readonly IRepaymentRepository _repaymentRepository;
        private readonly ILoanRepository _loanRepository;

        public RepaymentService(IRepaymentRepository repaymentRepository, ILoanRepository loanRepository)
        {
            _repaymentRepository = repaymentRepository;
            _loanRepository = loanRepository;
        }

        public async Task<Repayment> CreateAsync(Repayment repayment)
        {
            return await _repaymentRepository.CreateAsync(repayment);
        }

        public async Task<Repayment> UpdateAsync(Repayment repayment)
        {
            return await _repaymentRepository.EditAsync(repayment);
        }

        public async Task<IEnumerable<Repayment>> GetAllAsync()
        {
            return await _repaymentRepository.GetAllAsync();
        }

        public async Task<Repayment?> GetByIdAsync(int repaymentId)
        {
            return await _repaymentRepository.GetByIdAsync(repaymentId);
        }

        public async Task<IEnumerable<Repayment>> GetRepaymentsByLoanAsync(int loanId)
        {
            var allRepayments = await _repaymentRepository.GetAllAsync();
            return allRepayments.Where(r => r.LoanId == loanId);
        }

        public async Task MarkAsPaidAsync(int repaymentId)
        {
            var repayment = await _repaymentRepository.GetByIdAsync(repaymentId);
            if (repayment != null)
            {
                repayment.Status = RepaymentStatus.PAID;
                repayment.PaidDate = DateTime.UtcNow;
                await _repaymentRepository.EditAsync(repayment);

                var loan = await _loanRepository.GetByIdAsync(repayment.LoanId);
                if (loan != null && loan.Repayments.All(r => r.Status == RepaymentStatus.PAID))
                {
                    loan.Status = LoanStatus.CLOSED;
                    await _loanRepository.EditAsync(loan);
                }
            }
        }

        public async Task<IEnumerable<Repayment>> GetOverdueRepaymentsAsync()
        {
            var allRepayments = await _repaymentRepository.GetAllAsync();
            return allRepayments.Where(r => r.Status == RepaymentStatus.PENDING && r.DueDate < DateTime.UtcNow);
        }
    }
}