using AskMate.Common.Domain;
using AskMate.Web.Models;
using AskMate.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskMate.Web.Controllers

{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUsersService _usersService;

        public AccountController(ILogger<AccountController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel login, string returnUrl)
        {
            User user = _usersService.Login(login.Username, login.Password);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Username", user.Username),
                    }, CookieAuthenticationDefaults.AuthenticationScheme)),
                    new AuthenticationProperties());
                if (returnUrl == null)
                {
                    return LocalRedirect("/");
                }
                return LocalRedirect(returnUrl);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel register)
        {
            try
            {
                _usersService.Register(register.Username, register.Password);
            }
            catch (AskMateException)
            {
                ModelState.AddModelError("Username", "Already taken.");
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (returnUrl == null)
            {
                return LocalRedirect("/");
            }
            return LocalRedirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
