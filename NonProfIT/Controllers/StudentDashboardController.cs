using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NonProfIT.Controllers
{
    public class StudentDashboardController : Controller
    {
        [Authorize(Roles = "Student")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
