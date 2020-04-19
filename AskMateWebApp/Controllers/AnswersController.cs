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

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "answer-vote-up")]
        public IActionResult Up(int id)
        {
            _answersService.Vote(id, 1);
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "answer-vote-down")]
        public IActionResult Down(int id)
        {
            _answersService.Vote(id, -1);
            return Redirect(Request.Headers["Referer"]);
        }
    }
}
