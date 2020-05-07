using AskMateWebApp.Domain;
using AskMateWebApp.Models;
using AskMateWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Security.Claims;

namespace AskMateWebApp.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly IStorageService _storageService;
        private readonly IAnswersService _answersService;
        private readonly ICommentsService _commentsService;

        public AnswersController(
            ILogger<AnswersController> logger,
            IStorageService storageService,
            IAnswersService answerService,
            ICommentsService commentsService)
        {
            _logger = logger;
            _storageService = storageService;
            _answersService = answerService;
            _commentsService = commentsService;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var answer = _answersService.GetOne(id);
            return View(new AddAnswerModel(answer));
        }

        [HttpPost]
        public IActionResult Edit(int id, AddAnswerModel newAnswer)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            string imageFileName = newAnswer.Image?.FileName;
            using Stream imageStream = newAnswer.Image?.OpenReadStream();
            string image = imageFileName == null ? null : _storageService.Save(imageFileName, imageStream);
            try
            {
                _answersService.Update(userId, id, newAnswer.Message, image);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return RedirectToAction("Details", "Questions", new { id = newAnswer.QuestionId });
        }

        [HttpGet]
        [Route("[controller]/Add/[action]/{id}", Name = "add-answer-comment")]
        public IActionResult Comment(int id)
        {
            var answer = _answersService.GetOne(id);
            return View(new AddCommentModel
            {
                QuestionId = answer.QuestionId
            });
        }

        [HttpPost]
        [Route("[controller]/Add/[action]/{id}", Name = "add-answer-comment")]
        public IActionResult Comment(int id, AddCommentModel newComment)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            _commentsService.Add(userId, ICommentsService.CommentType.Answer, id, newComment.Message);
            return RedirectToAction("Details", "Questions", new { id = newComment.QuestionId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            Answer a = _answersService.GetOne(id);
            try
            {
                if (!string.IsNullOrEmpty(a.Image))
                {
                    _storageService.Delete(a.Image);
                }
                _commentsService.DeleteAll(userId, ICommentsService.CommentType.Answer, id);
                _answersService.Delete(userId, a.Id);
            }
            catch (AskMateNotAuthorizedException)
            {
                return Forbid();
            }
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "answer-vote-up")]
        public IActionResult Up(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _answersService.Vote(userId, id, 1);
            }
            catch (AskMateCannotVoteException)
            {
                return RedirectToAction("CannotVote");
            }
            return Redirect(Request.Headers["Referer"]);
        }

        [HttpPost]
        [Route("[controller]/Vote/[action]/{id}", Name = "answer-vote-down")]
        public IActionResult Down(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            try
            {
                _answersService.Vote(userId, id, -1);
            }
            catch (AskMateCannotVoteException)
            {
                return RedirectToAction("CannotVote");
            }
            return Redirect(Request.Headers["Referer"]);
        }
    }
}
