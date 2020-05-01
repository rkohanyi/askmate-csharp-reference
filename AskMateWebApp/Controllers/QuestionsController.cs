using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Models;
using AskMateWebApp.Services;
using AskMateWebApp.Domain;
using System.IO;

namespace AskMateWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IStorageService _storageService;
        private readonly IQuestionsService _questionsService;
        private readonly IAnswersService _answersService;
        private readonly ICommentsService _commentsService;
        private readonly ISearchService _searchService;

        public QuestionsController(
            ILogger<QuestionsController> logger,
            IStorageService storageService,
            IQuestionsService questionsService,
            IAnswersService answerService,
            ICommentsService commentsService,
            ISearchService searchService)
        {
            _logger = logger;
            _storageService = storageService;
            _questionsService = questionsService;
            _answersService = answerService;
            _commentsService = commentsService;
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questions = _questionsService.GetAll(5);
            return View(new QuestionListModel
            {
                Questions = questions.Select(x => new QuestionListItemModel(x)).ToList()
            });
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
        public IActionResult Search(string phrase)
        {
            var results = _searchService.SearchAll(phrase);
            return View(results.Select(x => new QuestionSearchResultModel(x.Key, x.Value)).ToList());
        }

        [HttpGet]
        public IActionResult Details(int id, Answer.SortField sort = AskMateWebApp.Domain.Answer.SortField.SubmissionTime, bool ascending = false)
        {
            var question = _questionsService.GetOne(id);
            var answers = _answersService.GetAll(id, sort, ascending);
            var questionComments = _commentsService.GetAll(ICommentsService.CommentType.Question, id);
            var answerComments = _commentsService.GetAll(ICommentsService.CommentType.Answer, answers.Select(x => x.Id).ToArray());
            _questionsService.View(id);
            return View(new QuestionDetailModel(question, questionComments, answers, answerComments));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddQuestionModel newQuestion)
        {
            string imageFileName = newQuestion.Image?.FileName;
            using Stream imageStream = newQuestion.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            int id = _questionsService.Add(newQuestion.Title, newQuestion.Message, image);
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
            string imageFileName = newQuestion.Image?.FileName;
            using Stream imageStream = newQuestion.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            _questionsService.Update(id, newQuestion.Title, newQuestion.Message, image);
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
            string imageFileName = newAnswer.Image?.FileName;
            using Stream imageStream = newAnswer.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            _answersService.Add(id, newAnswer.Message, image);
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        [Route("[controller]/Add/[action]/{id}", Name = "add-question-comment")]
        public IActionResult Comment(int id)
        {
            return View(new AddCommentModel
            {
                QuestionId = id
            });
        }

        [HttpPost]
        [Route("[controller]/Add/[action]/{id}", Name = "add-question-comment")]
        public IActionResult Comment(int id, AddCommentModel newComment)
        {
            _commentsService.Add(ICommentsService.CommentType.Question, id, newComment.Message);
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
            Question q = _questionsService.GetOne(id);
            foreach (var a in _answersService.GetAll(q.Id))
            {
                if (!string.IsNullOrEmpty(a.Image))
                {
                    _storageService.Delete(a.Image);
                }
            }
            foreach (var a in _answersService.GetAll(q.Id))
            {
                _commentsService.DeleteAll(ICommentsService.CommentType.Answer, a.Id);
            }
            _answersService.DeleteAll(id);
            if (!string.IsNullOrEmpty(q.Image))
            {
                _storageService.Delete(q.Image);
            }
            _commentsService.DeleteAll(ICommentsService.CommentType.Question, id);
            _questionsService.Delete(q.Id);
            if (redirect == null)
            {
                return Redirect(Request.Headers["Referer"]);
            }
            return LocalRedirect(redirect);
        }
    }
}
