// ✅ AuthController.cs
using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICustomerService _customerService;

        public AuthController(IAuthService authService, ICustomerService customerService)
        {
            _authService = authService;
            _customerService = customerService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Password hashing should be done here before saving customer
            var newCustomer = await _customerService.CreateAsync(customer);

            // ✅ Use UserId instead of CustomerId
            return CreatedAtAction(nameof(Register), new { id = newCustomer.UserId }, newCustomer);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var response = _authService.Login(model);
            if (response.IsSuccess)
                return Ok(response);

            return Unauthorized("Invalid credentials.");
        }
    }
}
