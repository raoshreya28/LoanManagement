using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface IRepaymentService
    {
        Task<Repayment> CreateAsync(Repayment repayment);
        Task<Repayment> UpdateAsync(Repayment repayment);
        Task<IEnumerable<Repayment>> GetAllAsync();
        Task<Repayment?> GetByIdAsync(int repaymentId);
        Task<IEnumerable<Repayment>> GetRepaymentsByLoanAsync(int loanApplicationId);
        Task MarkAsPaidAsync(int repaymentId);
        Task<IEnumerable<Repayment>> GetOverdueRepaymentsAsync();
    }
}
