using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface IRepaymentRepository
    {
        Task<Repayment> CreateAsync(Repayment repayment);
        Task<Repayment> EditAsync(Repayment repayment);
        Task DeleteAsync(int id);
        Task<IEnumerable<Repayment>> GetAllAsync();
        Task<Repayment?> GetByIdAsync(int id);
        Task<IEnumerable<Repayment>> GetRepaymentsWithLoanAndCustomerAsync();
    }
}