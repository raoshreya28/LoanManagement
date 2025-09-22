using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        // GET: api/Loan/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
                return NotFound($"Loan with ID {id} not found.");

            return Ok(loan);
        }

        // GET: api/Loan/customer/5
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetByCustomer(int customerId)
        {
            var loans = await _loanService.GetLoansByCustomerAsync(customerId);
            return Ok(loans);
        }

        // GET: api/Loan/npa
        [HttpGet("npa")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetNPALoans()
        {
            var npaLoans = await _loanService.GetNPALoansAsync();
            return Ok(npaLoans);
        }

        // POST: api/Loan
        [HttpPost]
        public async Task<ActionResult<Loan>> Create([FromBody] Loan loan)
        {
            if (loan == null)
                return BadRequest("Loan data is required.");

            var createdLoan = await _loanService.CreateAsync(loan);
            return CreatedAtAction(nameof(GetById), new { id = createdLoan.LoanId }, createdLoan);
        }

        // PUT: api/Loan/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Loan>> Update(int id, [FromBody] Loan loan)
        {
            if (loan == null || loan.LoanId != id)
                return BadRequest("Loan ID mismatch.");

            var updatedLoan = await _loanService.UpdateAsync(loan);
            if (updatedLoan == null)
                return NotFound($"Loan with ID {id} not found.");

            return Ok(updatedLoan);
        }
    }
}
