using Lending.Models;

namespace Lending.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int customerId);
        Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync(int customerId);
    }
}
