using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Models;
using AskMateWebApp.Services;
using AskMateWebApp.Domain;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AskMateWebApp.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IStorageService _storageService;
        private readonly IQuestionsService _questionsService;
        private readonly IQuestionsTagsService _questionsTagsService;
        private readonly ITagsService _tagsService;
        private readonly IAnswersService _answersService;
        private readonly ICommentsService _commentsService;
        private readonly ISearchService _searchService;

        public QuestionsController(
            ILogger<QuestionsController> logger,
            IStorageService storageService,
            IQuestionsService questionsService,
            IQuestionsTagsService questionsTagsService,
            ITagsService tagsService,
            IAnswersService answerService,
            ICommentsService commentsService,
            ISearchService searchService)
        {
            _logger = logger;
            _storageService = storageService;
            _tagsService = tagsService;
            _questionsService = questionsService;
            _questionsTagsService = questionsTagsService;
            _answersService = answerService;
            _commentsService = commentsService;
            _searchService = searchService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var questions = _questionsService.GetAll(new IQuestionsService.GetAllOptions { Limit = 5 });
            return View(new QuestionListModel
            {
                Questions = questions.Select(x => new QuestionListItemModel(x)).ToList()
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult List(Question.SortField sort = Question.SortField.SubmissionTime, bool ascending = false)
        {
            var questions = _questionsService.GetAll(new IQuestionsService.GetAllOptions(sort, ascending));
            return View(new QuestionListModel
            {
                SortField = sort,
                Ascending = ascending,
                Questions = questions.Select(x => new QuestionListItemModel(x)).ToList()
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search(string phrase)
        {
            var results = _searchService.SearchAll(phrase);
            return View(results.Select(x => new QuestionSearchResultModel(x.Key, x.Value)).ToList());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id, Answer.SortField sort = AskMateWebApp.Domain.Answer.SortField.SubmissionTime, bool ascending = false)
        {
            int? userId = int.TryParse(HttpContext.User.FindFirstValue("Id"), out var i) ? i : (int?)null;
            var question = _questionsService.GetOne(id);
            var tags = _tagsService.GetAll(id);
            var answers = _answersService.GetAll(new IAnswersService.GetAllOptions(sort, ascending) { QuestionId = id });
            var questionComments = _commentsService.GetAll(ICommentsService.CommentType.Question, id);
            var answerComments = _commentsService.GetAll(ICommentsService.CommentType.Answer, answers.Select(x => x.Id).ToArray());
            _questionsService.View(userId, id);
            return View(new QuestionDetailModel(question, tags, questionComments, answers, answerComments));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddQuestionModel newQuestion)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            string imageFileName = newQuestion.Image?.FileName;
            using Stream imageStream = newQuestion.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            int id = _questionsService.Add(userId, newQuestion.Title, newQuestion.Message, image);
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
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            string imageFileName = newQuestion.Image?.FileName;
            using Stream imageStream = newQuestion.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            try
            {
                _questionsService.Update(userId, id, newQuestion.Title, newQuestion.Message, image);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
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
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            string imageFileName = newAnswer.Image?.FileName;
            using Stream imageStream = newAnswer.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            _answersService.Add(userId, id, newAnswer.Message, image);
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
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            _commentsService.Add(userId, ICommentsService.CommentType.Question, id, newComment.Message);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "question-vote-up")]
        public IActionResult Up(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _questionsService.Vote(userId, id, 1);
            }
            catch (AskMateCannotVoteException)
            {
                return RedirectToAction("CannotVote");
            }
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "question-vote-down")]
        public IActionResult Down(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _questionsService.Vote(userId, id, -1);
            }
            catch (AskMateCannotVoteException)
            {
                return RedirectToAction("CannotVote");
            }
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpGet]
        public IActionResult CannotVote()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/Add/[action]/{id}", Name = "add-question-tag")]
        public IActionResult Tag()
        {
            return View();
        }

        [HttpPost]
        [Route("[controller]/Add/[action]/{id}", Name = "add-question-tag")]
        public IActionResult Tag(int id, AddTagModel newTag)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            string[] tagNames = newTag.Tags.Split(',').Select(x => x.Trim()).ToArray();
            int[] tagIds = _tagsService.Add(tagNames).ToArray();
            try
            {
                _questionsTagsService.Add(userId, id, tagIds);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Route("[controller]/{id}/Tags/{tagId}/Delete", Name = "delete-question-tag")]
        public IActionResult DeleteTag(int id, int tagId)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _questionsTagsService.Delete(userId, id, tagId);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public IActionResult Delete(int id, string redirect)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            Question q = _questionsService.GetOne(id);
            try
            {
                foreach (var a in _answersService.GetAll(new IAnswersService.GetAllOptions { QuestionId = q.Id }))
                {
                    if (!string.IsNullOrEmpty(a.Image))
                    {
                        _storageService.Delete(a.Image);
                    }
                }
                foreach (var a in _answersService.GetAll(new IAnswersService.GetAllOptions { QuestionId = q.Id }))
                {
                    _commentsService.DeleteAll(userId, ICommentsService.CommentType.Answer, a.Id);
                }
                _answersService.DeleteAll(userId, id);
                if (!string.IsNullOrEmpty(q.Image))
                {
                    _storageService.Delete(q.Image);
                }
                _commentsService.DeleteAll(userId, ICommentsService.CommentType.Question, id);
                _questionsTagsService.DeleteAll(q.Id);
                _questionsService.Delete(userId, q.Id);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            if (redirect == null)
            {
                return Redirect(Request.Headers["Referer"]);
            }
            return LocalRedirect(redirect);
        }
    }
}
