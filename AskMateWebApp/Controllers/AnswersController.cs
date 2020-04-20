using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Services;
using AskMateWebApp.Models;
using System.IO;

namespace AskMateWebApp.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly IStorageService _storageService;
        private readonly IAnswersService _answersService;

        public AnswersController(ILogger<AnswersController> logger, IStorageService storageService, IAnswersService answerService)
        {
            _logger = logger;
            _storageService = storageService;
            _answersService = answerService;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var answer = _answersService.GetOne(id);
            return View(new AddAnswerModel
            {
                QuestionId = answer.QuestionId,
                Message = answer.Message
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, AddAnswerModel newAnswer)
        {
            string imageFileName = newAnswer.Image?.FileName;
            using Stream imageStream = newAnswer.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            _answersService.Update(id, newAnswer.Message, image);
            return RedirectToAction("Details", "Questions", new { id = newAnswer.QuestionId });
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
