using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepaymentController : ControllerBase
    {
        private readonly IRepaymentService _repaymentService;

        public RepaymentController(IRepaymentService repaymentService)
        {
            _repaymentService = repaymentService;
        }

        [HttpGet("loan/{loanId}")]
        public async Task<ActionResult<IEnumerable<Repayment>>> GetByLoan(int loanId)
        {
            return Ok(await _repaymentService.GetRepaymentsByLoanAsync(loanId));
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            await _repaymentService.MarkAsPaidAsync(id);
            return Ok($"Repayment {id} marked as paid.");
        }
    }
}
