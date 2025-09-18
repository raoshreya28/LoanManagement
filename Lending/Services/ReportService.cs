using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Report> GenerateReportAsync(ReportType type)
        {
            var report = new Report
            {
                Type = type,
                Title = $"{type} Report - {DateTime.UtcNow:dd/MM/yyyy}",
                Url = $"Reports/{type}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf",
                GeneratedAt = DateTime.UtcNow
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Reports
                                 .Include(r => r.GeneratedBy)
                                 .ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int reportId)
        {
            return await _context.Reports
                                 .Include(r => r.GeneratedBy)
                                 .FirstOrDefaultAsync(r => r.ReportId == reportId);
        }
    }
}
