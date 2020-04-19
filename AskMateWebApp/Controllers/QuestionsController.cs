using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Models;
using AskMateWebApp.Services;
using AskMateWebApp.Domain;

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
        public IActionResult List(Question.SortField sort = Question.SortField.SubmissionTime, bool ascending = false)
        {
            var questions = _questionsService.GetAll(sort, ascending);
            return View(new QuestionListModel
            {
                SortField = sort,
                Ascending = ascending,
                Questions = questions.Select(x => new QuestionListItemModel(x)).ToList()
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var question = _questionsService.GetOne(id);
            var answers = _answersService.GetAll(id);
            _questionsService.View(id);
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
        public IActionResult Edit(int id)
        {
            var question = _questionsService.GetOne(id);
            return View(new AddQuestionModel
            {
                Title = question.Title,
                Message = question.Message
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, AddQuestionModel newQuestion)
        {
            _questionsService.Update(id, newQuestion.Title, newQuestion.Message);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("[controller]/Add/[action]/{id}", Name = "add-answer")]
        public IActionResult Answer()
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

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "question-vote-up")]
        public IActionResult Up(int id)
        {
            _questionsService.Vote(id, 1);
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "question-vote-down")]
        public IActionResult Down(int id)
        {
            _questionsService.Vote(id, -1);
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        public IActionResult Delete(int id, string redirect)
        {
            _questionsService.Delete(id);
            _answersService.DeleteAll(id);
            if (redirect == null)
            {
                return Redirect(Request.Headers["Referer"]);
            }
            return LocalRedirect(redirect);
        }
    }
}
