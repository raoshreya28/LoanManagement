using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lending.Repositories;

namespace Lending.Services
{
    public class LoanOfficerService : ILoanOfficerService
    {
        private readonly ILoanOfficerRepository _loanOfficerRepository;
        private readonly ICustomerRepository _customerRepository;

        public LoanOfficerService(ILoanOfficerRepository loanOfficerRepository, ICustomerRepository customerRepository)
        {
            _loanOfficerRepository = loanOfficerRepository;
            _customerRepository = customerRepository;
        }

        public async Task<LoanOfficer> CreateAsync(LoanOfficer officer)
        {
            return await _loanOfficerRepository.CreateAsync(officer);
        }

        public async Task<LoanOfficer> UpdateAsync(LoanOfficer officer)
        {
            return await _loanOfficerRepository.EditAsync(officer);
        }

        public async Task DeleteAsync(int officerId)
        {
            await _loanOfficerRepository.DeleteAsync(officerId);
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync()
        {
            return await _loanOfficerRepository.GetAllAsync();
        }

        public async Task<LoanOfficer?> GetByIdAsync(int officerId)
        {
            return await _loanOfficerRepository.GetByIdAsync(officerId);
        }

        // ✅ Auto-assign logic
        public async Task<LoanOfficer?> AssignLoanAsync(LoanApplication application)
        {
            var customer = await _customerRepository.GetByIdAsync(application.CustomerId);
            if (customer == null)
            {
                // Handle case where customer does not exist, though it shouldn't happen with proper foreign keys.
                return null;
            }

            // Find officers in the same city with the fewest current assignments
            var availableOfficers = await _loanOfficerRepository.GetAllAsync();

            var assignedOfficer = availableOfficers
                .Where(o => o.LoanOfficerCity == customer.CustomerCity && o.IsAvailable)
                .OrderBy(o => o.CurrentAssignments)
                .FirstOrDefault();

            if (assignedOfficer != null)
            {
                // Assign the loan
                assignedOfficer.CurrentAssignments += 1;

                // Update the officer's availability if a certain threshold is reached
                if (assignedOfficer.CurrentAssignments >= 10) // example limit from previous comment
                    assignedOfficer.IsAvailable = false;

                await _loanOfficerRepository.EditAsync(assignedOfficer);

                // Update the application with the assigned officer's ID
                application.LoanOfficerId = assignedOfficer.UserId;
            }
            return assignedOfficer;
        }
    }
}