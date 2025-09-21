using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                return Ok(await _customerService.GetAllAsync());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching all customers.");
                return Problem("An error occurred while fetching customers.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                    return NotFound($"Customer with ID {id} not found.");

                return Ok(customer);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching customer {CustomerId}", id);
                return Problem($"An error occurred while fetching customer {id}.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest("Customer data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
                var created = await _customerService.CreateAsync(customer);
                return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating customer.");
                return Problem("An error occurred while creating the customer.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.UserId != id)
                return BadRequest("Customer ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _customerService.UpdateAsync(customer);
                if (updated == null)
                    return NotFound($"Customer with ID {id} not found.");

                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error updating customer {CustomerId}", id);
                return Problem($"An error occurred while updating customer {id}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null) return NotFound();

                customer.IsActive = false;
                await _customerService.UpdateAsync(customer);

                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer {CustomerId}", id);
                return Problem($"An error occurred while deleting customer {id}.");
            }
        }

        [HttpGet("{id}/loan-applications")]
        public async Task<ActionResult<IEnumerable<LoanApplication>>> GetLoanApplications(int id)
        {
            try
            {
                return Ok(await _customerService.GetLoanApplicationsAsync(id));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan applications for customer {CustomerId}", id);
                return Problem($"An error occurred while fetching loan applications for customer {id}.");
            }
        }
    }
}
