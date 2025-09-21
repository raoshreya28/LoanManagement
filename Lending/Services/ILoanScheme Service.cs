using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface ILoanSchemeService
    {
        Task<LoanScheme> CreateAsync(LoanScheme scheme);
        Task<LoanScheme> UpdateAsync(LoanScheme scheme);
        Task DeleteAsync(int schemeId);
        Task<IEnumerable<LoanScheme>> GetAllAsync();
        Task<LoanScheme?> GetByIdAsync(int schemeId);
    }
}







