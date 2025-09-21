using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanSchemeService : ILoanSchemeService
    {
        private readonly ILoanSchemeRepository _loanSchemeRepository;

        public LoanSchemeService(ILoanSchemeRepository loanSchemeRepository)
        {
            _loanSchemeRepository = loanSchemeRepository;
        }

        public async Task<LoanScheme> CreateAsync(LoanScheme scheme)
        {
            return await _loanSchemeRepository.CreateAsync(scheme);
        }

        public async Task<LoanScheme> UpdateAsync(LoanScheme scheme)
        {
            return await _loanSchemeRepository.EditAsync(scheme);
        }

        public async Task DeleteAsync(int schemeId)
        {
            await _loanSchemeRepository.DeleteAsync(schemeId);
        }

        public async Task<IEnumerable<LoanScheme>> GetAllAsync()
        {
            return await _loanSchemeRepository.GetAllAsync();
        }

        public async Task<LoanScheme?> GetByIdAsync(int schemeId)
        {
            return await _loanSchemeRepository.GetByIdAsync(schemeId);
        }
    }
}