using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface IReportRepository
    {
        Task<Report> CreateAsync(Report report);
        Task<Report> EditAsync(Report report);
        Task DeleteAsync(int id);
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report?> GetByIdAsync(int id);
    }
}
