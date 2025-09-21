using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanOfficerController : ControllerBase
    {
        private readonly ILoanOfficerService _loanOfficerService;
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanOfficerController(ILoanOfficerService loanOfficerService, ILoanApplicationService loanApplicationService)
        {
            _loanOfficerService = loanOfficerService;
            _loanApplicationService = loanApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanOfficer>>> GetAll()
        {
            return Ok(await _loanOfficerService.GetAllAsync());
        }

        [HttpPost("{loanApplicationId}/assign")]
        public async Task<IActionResult> AssignLoan(int loanApplicationId)
        {
            // 1. Fetch the LoanApplication from DB using the repository
            var application = await _loanApplicationService.GetByIdAsync(loanApplicationId);

            if (application == null)
                return NotFound($"Loan application {loanApplicationId} not found.");

            // 2. Pass it to the service for assignment
            var assignedOfficer = await _loanOfficerService.AssignLoanAsync(application);

            if (assignedOfficer == null)
                return NotFound("No available loan officer found in this city.");

            // 3. Return success message
            return Ok(new
            {
                Message = $"Loan application {loanApplicationId} assigned successfully.",
                Officer = assignedOfficer
            });
        }

    }
}
