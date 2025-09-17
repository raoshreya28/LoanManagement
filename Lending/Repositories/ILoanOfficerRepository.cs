using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface ILoanOfficerRepository
    {
        Task<LoanOfficer> CreateAsync(LoanOfficer officer);
        Task<LoanOfficer> EditAsync(LoanOfficer officer);
        Task DeleteAsync(int id);
        Task<IEnumerable<LoanOfficer>> GetAllAsync();
        Task<LoanOfficer?> GetByIdAsync(int id);
    }
}
