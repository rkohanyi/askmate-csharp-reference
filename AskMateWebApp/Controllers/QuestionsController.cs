using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Models;
using AskMateWebApp.Services;

namespace AskMateWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IQuestionsService _questionsService;

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionsService questionsService)
        {
            _logger = logger;
            _questionsService = questionsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var questions = _questionsService.GetAll();
            return View(questions.Select(x => new QuestionModel(x)).ToList());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var question = _questionsService.GetOne(id);
            return View(new QuestionModel(question));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddQuestionModel newQuestion)
        {
            int id = _questionsService.Add(newQuestion.Title, newQuestion.Message);
            return RedirectToAction("Get", new { id });
        }
    }
}
