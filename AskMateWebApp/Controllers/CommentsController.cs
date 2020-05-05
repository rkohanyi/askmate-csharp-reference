using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AskMateWebApp.Services;
using AskMateWebApp.Models;

namespace AskMateWebApp.Controllers
{
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
            AddCommentModel model;
            if (comment.AnswerId > 0)
            {
                var answer = _answersService.GetOne((int)comment.AnswerId);
                model = new AddCommentModel(answer.QuestionId, comment.Message);
            }
            else
            {
                model = new AddCommentModel(id, comment.Message);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, AddCommentModel newComment)
        {
            _commentsService.Update(id, newComment.Message);
            return RedirectToAction("Details", "Questions", new { id = newComment.QuestionId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _commentsService.Delete(id);
            return Redirect(Request.Headers["Referer"]);
        }
    }
}
