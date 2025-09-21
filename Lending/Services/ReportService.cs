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
            var report = new Report
            {
                Type = type,
                Title = $"{type} Report - {DateTime.UtcNow:dd/MM/yyyy}",
                Url = $"Reports/{type}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf",
                GeneratedAt = DateTime.UtcNow,
                GeneratedById = generatedById
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