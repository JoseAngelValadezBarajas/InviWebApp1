using inviWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inviWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly inviContext _context;
        public LoginController(inviContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(login _login)
        {
            var loginP = await _context.Login.FindAsync(1);
            if(loginP != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(_login);
            }
        }
    }
}
