using AskMateWebApp.Models;
using AskMateWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AskMateWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;
        private readonly IQuestionsService _questionsService;
        private readonly IAnswersService _answersService;
        private readonly ICommentsService _commentsService;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersService usersService,
            IQuestionsService questionsService,
            IAnswersService answersService,
            ICommentsService commentsService)
        {
            _logger = logger;
            _usersService = usersService;
            _questionsService = questionsService;
            _answersService = answersService;
            _commentsService = commentsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _usersService.GetAll();
            return View(new UserListModel
            {
                Users = users.Select(x => new UserListItemModel(x)).ToList()
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _usersService.GetOne(id);
            var questions = _questionsService.GetAll(new IQuestionsService.GetAllOptions { UserId = id });
            var answers = _answersService.GetAll(new IAnswersService.GetAllOptions { UserId = id });
            var comments = _commentsService.GetAll(id);
            return View(new UserDetailModel(user, questions, answers, comments));
        }
    }
}
