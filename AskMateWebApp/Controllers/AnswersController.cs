using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Services;

namespace AskMateWebApp.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly IAnswersService _answersService;

        public AnswersController(ILogger<AnswersController> logger, IAnswersService answerService)
        {
            _logger = logger;
            _answersService = answerService;
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answersService.Delete(id);
            return Redirect(Request.Headers["Referer"]);
        }
    }
}