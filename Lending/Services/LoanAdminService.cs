using Lending.Data;
using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Repositories;

namespace Lending.Services
{
    public class LoanAdminService : ILoanAdminService
    {
        private readonly ILoanAdminRepository _loanAdminRepository;

        public LoanAdminService(ILoanAdminRepository loanAdminRepository)
        {
            _loanAdminRepository = loanAdminRepository;
        }

        public async Task<LoanAdmin> CreateAsync(LoanAdmin admin)
        {
            return await _loanAdminRepository.CreateAsync(admin);
        }

        public async Task<LoanAdmin> UpdateAsync(LoanAdmin admin)
        {
            return await _loanAdminRepository.EditAsync(admin);
        }

        public async Task DeleteAsync(int adminId)
        {
            await _loanAdminRepository.DeleteAsync(adminId);
        }

        public async Task<IEnumerable<LoanAdmin>> GetAllAsync()
        {
            return await _loanAdminRepository.GetAllAsync();
        }

        public async Task<LoanAdmin?> GetByIdAsync(int adminId)
        {
            return await _loanAdminRepository.GetByIdAsync(adminId);
        }
    }
}