using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface ILoanAdminRepository
    {
        Task<LoanAdmin> CreateAsync(LoanAdmin admin);
        Task<LoanAdmin> EditAsync(LoanAdmin admin);
        Task DeleteAsync(int id);
        Task<IEnumerable<LoanAdmin>> GetAllAsync();
        Task<LoanAdmin?> GetByIdAsync(int id);
    }
}
