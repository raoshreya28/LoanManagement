using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lending.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanOfficerService _officerService;

        public LoanApplicationService(
            ILoanApplicationRepository loanApplicationRepository,
            ILoanRepository loanRepository,
            ILoanOfficerService officerService)
        {
            _loanApplicationRepository = loanApplicationRepository;
            _loanRepository = loanRepository;
            _officerService = officerService;
        }

        // Create loan application + auto-assign officer
        public async Task<LoanApplication> CreateAsync(LoanApplication application)
        {
            var createdApplication = await _loanApplicationRepository.CreateAsync(application);

            // Auto-assign officer
            await _officerService.AssignLoanAsync(createdApplication);

            return createdApplication;
        }

        public async Task<LoanApplication> UpdateAsync(LoanApplication application)
        {
            return await _loanApplicationRepository.EditAsync(application);
        }

        public async Task DeleteAsync(int applicationId)
        {
            await _loanApplicationRepository.DeleteAsync(applicationId);
        }

        public async Task<IEnumerable<LoanApplication>> GetAllAsync()
        {
            return await _loanApplicationRepository.GetAllAsync();
        }

        public async Task<LoanApplication?> GetByIdAsync(int applicationId)
        {
            return await _loanApplicationRepository.GetByIdAsync(applicationId);
        }

        public async Task<IEnumerable<LoanApplication>> GetPendingApplicationsAsync()
        {
            var allApplications = await _loanApplicationRepository.GetAllAsync();
            return allApplications.Where(l => l.Status == LoanStatus.PENDING);
        }

        // Corrected ApproveLoanAsync to also create a Loan record
        public async Task ApproveLoanAsync(int applicationId)
        {
            var loanApp = await _loanApplicationRepository.GetByIdAsync(applicationId);
            if (loanApp != null)
            {
                loanApp.Status = LoanStatus.APPROVED;
                await _loanApplicationRepository.EditAsync(loanApp);

                // Create a new Loan entry when the application is approved
                var newLoan = new Loan
                {
                    LoanApplicationId = loanApp.LoanApplicationId,
                    ApprovedAmount = loanApp.AppliedAmount, // Use ApprovedAmount from model
                    LoanOfficerId = loanApp.LoanOfficerId.Value, // Ensure officer is assigned
                    DisbursementDate = System.DateTime.UtcNow,
                    Status = LoanStatus.APPROVED // Correctly set the loan status
                };
                await _loanRepository.CreateAsync(newLoan);
            }
        }

        public async Task RejectLoanAsync(int applicationId, string remarks)
        {
            var loanApp = await _loanApplicationRepository.GetByIdAsync(applicationId);
            if (loanApp != null)
            {
                loanApp.Status = LoanStatus.REJECTED;
                loanApp.Remarks = remarks;
                await _loanApplicationRepository.EditAsync(loanApp);
            }
        }
    }
}