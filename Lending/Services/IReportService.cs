using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface IReportService
    {
        Task<Report> GenerateReportAsync(ReportType type);
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report?> GetByIdAsync(int reportId);
    }
}
