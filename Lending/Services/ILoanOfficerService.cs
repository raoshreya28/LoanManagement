using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface ILoanOfficerService
    {
        Task<LoanOfficer> CreateAsync(LoanOfficer officer);
        Task<LoanOfficer> UpdateAsync(LoanOfficer officer);
        Task DeleteAsync(int officerId);
        Task<IEnumerable<LoanOfficer>> GetAllAsync();
        Task<LoanOfficer?> GetByIdAsync(int officerId);

        // Auto-assigns the loan officer based on city and availability
        Task<LoanOfficer?> AssignLoanAsync(LoanApplication application);
    }
}