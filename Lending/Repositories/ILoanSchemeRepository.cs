using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface ILoanSchemeRepository
    {
        Task<LoanScheme> CreateAsync(LoanScheme scheme);
        Task<LoanScheme> EditAsync(LoanScheme scheme);
        Task DeleteAsync(int id);
        Task<IEnumerable<LoanScheme>> GetAllAsync();
        Task<LoanScheme?> GetByIdAsync(int id);
    }
}
