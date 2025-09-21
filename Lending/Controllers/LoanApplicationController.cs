using Microsoft.AspNetCore.Mvc;

namespace Lending.Controllers
{
    public class LoanApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
