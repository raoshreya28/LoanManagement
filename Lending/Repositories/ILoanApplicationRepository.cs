using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface ILoanApplicationRepository
    {
        Task<LoanApplication> CreateAsync(LoanApplication application);
        Task<LoanApplication> EditAsync(LoanApplication application);
        Task DeleteAsync(int id);
        Task<IEnumerable<LoanApplication>> GetAllAsync();
        Task<LoanApplication?> GetByIdAsync(int id);
    }
}
