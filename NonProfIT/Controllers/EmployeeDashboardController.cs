using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NonProfIT.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        [Authorize(Roles = "Employee")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
