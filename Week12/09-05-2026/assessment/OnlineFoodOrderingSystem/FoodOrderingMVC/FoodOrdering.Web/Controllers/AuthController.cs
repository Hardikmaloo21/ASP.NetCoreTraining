using FoodOrdering.Web.Models;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
        }

        // =========================
        // LOGIN GET
        // =========================

        public IActionResult Login()
        {
            return View();
        }

        // =========================
        // LOGIN POST
        // =========================

        [HttpPost]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {
            var result =
                await _signInManager.PasswordSignInAsync(
                    email,
                    password,
                    false,
                    false);

            if (result.Succeeded)
            {
                return RedirectToAction(
                    "Index",
                    "Home");
            }

            ViewBag.Error = "Invalid Login";

            return View();
        }

        // =========================
        // REGISTER GET
        // =========================

        public IActionResult Register()
        {
            return View();
        }

        // =========================
        // REGISTER POST
        // =========================

        [HttpPost]
        public async Task<IActionResult> Register(
            ApplicationUser user,
            string password)
        {
            user.UserName = user.Email;

            var result =
                await _userManager.CreateAsync(
                    user,
                    password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // =========================
        // LOGOUT
        // =========================

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                "Login",
                "Auth");
        }
    }
}