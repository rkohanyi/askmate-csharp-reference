using AskMate.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AskMate.Web.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDatabaseService _databaseService;

        public DatabaseController(
            ILogger<DatabaseController> logger,
            IWebHostEnvironment environment,
            IDatabaseService databaseService)
        {
            _logger = logger;
            _environment = environment;
            _databaseService = databaseService;
        }

        [HttpPost]
        public async Task<IActionResult> ResetAsync()
        {
            if (!_environment.IsDevelopment())
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            _databaseService.Reset();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("List", "Questions");
        }
    }
}
