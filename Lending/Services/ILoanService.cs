using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface ILoanService
    {
        Task<Loan> CreateAsync(Loan loan);
        Task<Loan> UpdateAsync(Loan loan);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int loanId);
        Task<IEnumerable<Loan>> GetLoansByCustomerAsync(int customerId);
        Task<IEnumerable<Loan>> GetNPALoansAsync(); // NPA logic
    }
}
