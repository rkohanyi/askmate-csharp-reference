using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Services;
using AskMateWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AskMateWebApp.Domain;

namespace AskMateWebApp.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IAnswersService _answersService;
        private readonly ICommentsService _commentsService;

        public CommentsController(ILogger<CommentsController> logger, IAnswersService answersService, ICommentsService commentsService)
        {
            _logger = logger;
            _answersService = answersService;
            _commentsService = commentsService;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var comment = _commentsService.GetOne(id);
            return View(new AddCommentModel(comment.QuestionId, comment.Message));
        }

        [HttpPost]
        public IActionResult Edit(int id, AddCommentModel newComment)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _commentsService.Update(userId, id, newComment.Message);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return RedirectToAction("Details", "Questions", new { id = newComment.QuestionId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _commentsService.Delete(userId, id);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return Redirect(Request.Headers["Referer"]);
        }
    }
}
