using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> CreateAsync(Loan loan);
        Task<Loan> EditAsync(Loan loan);
        Task DeleteAsync(int id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
    }
}
