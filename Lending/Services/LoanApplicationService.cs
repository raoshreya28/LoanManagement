using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly AppDbContext _context;
        private readonly ILoanOfficerService _officerService;

        public LoanApplicationService(AppDbContext context, ILoanOfficerService officerService)
        {
            _context = context;
            _officerService = officerService;
        }

        // Create loan application + auto-assign officer
        public async Task<LoanApplication> CreateAsync(LoanApplication application)
        {
            await _context.LoanApplications.AddAsync(application);
            await _context.SaveChangesAsync();

            // Auto-assign officer and get the officer back
            var assignedOfficer = await _officerService.AssignLoanAsync(application);

            if (assignedOfficer != null)
            {
                // Already updated in AssignLoanAsync
                // You can log or notify if needed
            }

            return application;
        }


        // Update application
        public async Task<LoanApplication> UpdateAsync(LoanApplication application)
        {
            _context.LoanApplications.Update(application);
            await _context.SaveChangesAsync();
            return application;
        }

        // Delete application
        public async Task DeleteAsync(int applicationId)
        {
            var application = await _context.LoanApplications.FindAsync(applicationId);
            if (application != null)
            {
                _context.LoanApplications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }

        // Get all applications with includes
        public async Task<IEnumerable<LoanApplication>> GetAllAsync()
        {
            return await _context.LoanApplications
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanScheme)
                                 .Include(l => l.LoanOfficer)
                                 .ToListAsync();
        }

        // Get by Id
        public async Task<LoanApplication?> GetByIdAsync(int applicationId)
        {
            return await _context.LoanApplications
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanScheme)
                                 .Include(l => l.LoanOfficer)
                                 .FirstOrDefaultAsync(l => l.LoanApplicationId == applicationId);
        }

        // Get pending applications (for officers)
        public async Task<IEnumerable<LoanApplication>> GetPendingApplicationsAsync()
        {
            return await _context.LoanApplications
                                 .Where(l => l.Status == LoanStatus.PENDING)
                                 .Include(l => l.Customer)
                                 .Include(l => l.LoanOfficer)
                                 .Include(l => l.LoanScheme)
                                 .ToListAsync();
        }

        // Approve loan
        public async Task ApproveLoanAsync(int applicationId)
        {
            var loanApp = await _context.LoanApplications.FindAsync(applicationId);
            if (loanApp != null)
            {
                loanApp.Status = LoanStatus.APPROVED;
                await _context.SaveChangesAsync();
            }
        }

        // Reject loan
        public async Task RejectLoanAsync(int applicationId, string remarks)
        {
            var loanApp = await _context.LoanApplications.FindAsync(applicationId);
            if (loanApp != null)
            {
                loanApp.Status = LoanStatus.REJECTED;
                loanApp.Remarks = remarks;
                await _context.SaveChangesAsync();
            }
        }

        // Auto-assign officer based on customer city
        public async Task AutoAssignOfficerAsync(LoanApplication application)
        {
            if (application.LoanOfficerId != null) return; // already assigned

            var assignedOfficer = await _officerService.AssignLoanAsync(application);

            if (assignedOfficer != null)
            {
                application.LoanOfficerId = assignedOfficer.UserId;
                await _context.SaveChangesAsync();
            }
        }

        // Optional: get NPA loans (overdue repayments)
        public async Task<IEnumerable<LoanApplication>> GetNPAApplicationsAsync()
        {
            var today = System.DateTime.UtcNow;
            return await _context.LoanApplications
                                 .Include(l => l.Repayments)
                                 .Include(l => l.Customer)
                                 .Where(l => l.Repayments.Any(r => r.DueDate < today && r.Status != RepaymentStatus.PAID))
                                 .ToListAsync();
        }
    }
}
