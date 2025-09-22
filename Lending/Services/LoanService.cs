using Lending.Models;
﻿using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public LoanService(ILoanRepository loanRepository, ILoanApplicationRepository loanApplicationRepository)
        {
            _loanRepository = loanRepository;
            _loanApplicationRepository = loanApplicationRepository;
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            return await _loanRepository.CreateAsync(loan);
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            return await _loanRepository.EditAsync(loan);
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {

            return await _loanRepository.GetAllAsync();
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _loanRepository.GetByIdAsync(loanId);
        }

        public async Task<IEnumerable<Loan>> GetLoansByCustomerAsync(int customerId)
        {
            var allLoans = await _loanRepository.GetAllAsync();
            return allLoans.Where(l => l.LoanApplication?.CustomerId == customerId);
        }

        public async Task<IEnumerable<Loan>> GetNPALoansAsync()
        {
            var allLoans = await _loanRepository.GetAllAsync();
            return allLoans.Where(l => l.Repayments.Any(r => r.Status == RepaymentStatus.OVERDUE));
        }
    }
}