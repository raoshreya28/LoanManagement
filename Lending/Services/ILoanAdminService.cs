using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface ILoanAdminService
    {
        Task<LoanAdmin> CreateAsync(LoanAdmin admin);
        Task<LoanAdmin> UpdateAsync(LoanAdmin admin);
        Task DeleteAsync(int adminId);
        Task<IEnumerable<LoanAdmin>> GetAllAsync();
        Task<LoanAdmin?> GetByIdAsync(int adminId);
    }
}
