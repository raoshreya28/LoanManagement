using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface ILoanApplicationService
    {
        Task<LoanApplication> CreateAsync(LoanApplication application);
        Task<LoanApplication> UpdateAsync(LoanApplication application);
        Task DeleteAsync(int applicationId);
        Task<IEnumerable<LoanApplication>> GetAllAsync();
        Task<LoanApplication?> GetByIdAsync(int applicationId);
        Task<IEnumerable<LoanApplication>> GetPendingApplicationsAsync();
        Task ApproveLoanAsync(int applicationId);
        Task RejectLoanAsync(int applicationId, string remarks);
        Task AutoAssignOfficerAsync(LoanApplication application); // Auto-assign logic
    }
}
