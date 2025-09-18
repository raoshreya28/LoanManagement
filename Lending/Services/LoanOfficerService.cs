using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class LoanOfficerService : ILoanOfficerService
    {
        private readonly AppDbContext _context;

        public LoanOfficerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanOfficer> CreateAsync(LoanOfficer officer)
        {
            await _context.LoanOfficers.AddAsync(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task<LoanOfficer> UpdateAsync(LoanOfficer officer)
        {
            _context.LoanOfficers.Update(officer);
            await _context.SaveChangesAsync();
            return officer;
        }

        public async Task DeleteAsync(int officerId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer != null)
            {
                _context.LoanOfficers.Remove(officer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync()
        {
            return await _context.LoanOfficers.ToListAsync();
        }

        public async Task<LoanOfficer?> GetByIdAsync(int officerId)
        {
            return await _context.LoanOfficers.FindAsync(officerId);
        }

        // ✅ Auto-assign logic
        public async Task<LoanOfficer?> AssignLoanAsync(LoanApplication application)
        {
            if (application.Customer == null)
            {
                // Load customer info if not included
                application.Customer = await _context.Customers.FindAsync(application.CustomerId);
            }

            // Find officers in the same city who are available
            var availableOfficers = await _context.LoanOfficers
                .Where(o => o.LoanOfficerCity == application.Customer.CustomerCity && o.IsAvailable)
                .OrderBy(o => o.CurrentAssignments)
                .ToListAsync();

            var assignedOfficer = availableOfficers.FirstOrDefault();

            if (assignedOfficer != null)
            {
                // Assign the loan
                assignedOfficer.CurrentAssignments += 1;

                // If officer reaches max assignment (optional), mark as unavailable
                if (assignedOfficer.CurrentAssignments >= 10) // example limit
                    assignedOfficer.IsAvailable = false;

                // Save officer changes
                _context.LoanOfficers.Update(assignedOfficer);

                // Update application
                application.LoanOfficerId = assignedOfficer.LoanOfficerId;
                _context.LoanApplications.Update(application);

                await _context.SaveChangesAsync();
            }

            return assignedOfficer;
        }
    }
}
