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
        private readonly IAnswersService _answersService;

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionsService questionsService, IAnswersService answerService)
        {
            _logger = logger;
            _questionsService = questionsService;
            _answersService = answerService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var questions = _questionsService.GetAll();
            return View(questions.Select(x => new QuestionListItemModel(x)).ToList());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var question = _questionsService.GetOne(id);
            var answers = _answersService.GetAll(id);
            return View(new QuestionDetailModel(question, answers));
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
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("[controller]/Add/[action]/{id}", Name = "add-answer")]
        public IActionResult Answer(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("[controller]/Add/[action]/{id}", Name = "add-answer")]
        public IActionResult Answer(int id, AddAnswerModel newAnswer)
        {
            _answersService.Add(id, newAnswer.Message);
            return RedirectToAction("Details", new { id });
        }
    }
}
