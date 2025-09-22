using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanAdminRepository _loanAdminRepository;

        public ReportService(IReportRepository reportRepository, ILoanRepository loanRepository, ILoanAdminRepository loanAdminRepository)
        {
            _reportRepository = reportRepository;
            _loanRepository = loanRepository;
            _loanAdminRepository = loanAdminRepository;
        }

        

        public async Task<Report> GenerateReportAsync(ReportType type, int generatedById)
        {
            // Note: This is a placeholder for the actual report generation logic (e.g., using a reporting library).
            // For now, it just creates a record of the report. The actual data extraction would happen here.
            var report = new Report
            {
                Type = type,
                Title = $"{type} Report - {DateTime.UtcNow:dd/MM/yyyy}",
                // In a real application, you would generate a file and save its path
                Url = $"Reports/{type}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf",
                GeneratedAt = DateTime.UtcNow,
                GeneratedById = generatedById
                // The correct foreign key property to use.
                // Assuming a LoanAdmin is generating the report
                GeneratedById = 1 // Placeholder for a real user ID
            };

            return await _reportRepository.CreateAsync(report);
        }


        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _reportRepository.GetAllAsync();
        }

        public async Task<Report?> GetByIdAsync(int reportId)
        {
            return await _reportRepository.GetByIdAsync(reportId);
        }
    }
}