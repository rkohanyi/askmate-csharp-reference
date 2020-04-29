using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AskMateWebApp.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDbConnection _connection;

        public DatabaseController(
            ILogger<DatabaseController> logger,
            IWebHostEnvironment environment,
            IDbConnection connection)
        {
            _logger = logger;
            _environment = environment;
            _connection = connection;
        }

        [HttpPost]
        public IActionResult Reset()
        {
            if (!_environment.IsDevelopment())
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            _logger.LogDebug("Resetting database");
            foreach (string script in new string[] { "drop-init", "question", "answer", "tag", "question_tag", "comment" })
            {
                _logger.LogDebug($"About to run {script}.sql");
                using IDbCommand command = _connection.CreateCommand();
                command.CommandText = System.IO.File.ReadAllText($"{script}.sql");
                command.ExecuteNonQuery();
                _logger.LogDebug($"Successfully run {script}.sql");
            }
            return RedirectToAction("List", "Questions");
        }
    }
}
