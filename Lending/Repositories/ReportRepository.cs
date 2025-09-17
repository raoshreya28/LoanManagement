using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;
        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports
                                 .Include(r => r.GeneratedBy)
                                 .ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _context.Reports
                                 .Include(r => r.GeneratedBy)
                                 .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task<Report> CreateAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> EditAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id);
            if (existing != null)
            {
                _context.Reports.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
