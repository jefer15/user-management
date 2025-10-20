using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Create() => View();
        public IActionResult Manage() => View();
    }
}
