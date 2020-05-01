using AskMateWebApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AskMateWebApp.Controllers
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
        public IActionResult Reset()
        {
            if (!_environment.IsDevelopment())
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            _databaseService.Reset();
            return RedirectToAction("List", "Questions");
        }
    }
}
