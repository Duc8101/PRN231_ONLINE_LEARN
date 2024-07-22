using Common.Base;
using Common.Entity;
using Common.Enums;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.TakeQuiz;

namespace WEB_CLIENT.Controllers
{
    [Role(Roles.Student)]
    [Authorize]
    [ResponseCache(NoStore = true)]
    public class TakeQuizController : BaseController
    {
        private readonly ITakeQuizService _service;

        public TakeQuizController(ITakeQuizService service)
        {
            _service = service;
        }

        public ActionResult Index(Guid? id /*LessonID*/)
        {
            string? studentId = getUserId();
            if (studentId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null)
            {
                return Redirect("/MyCourse");
            }
            ResponseBase<Dictionary<string, object?>?> response = _service.Index(id.Value, Guid.Parse(studentId));
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Index(Guid id /*LessonID*/, string? button, int? answer, int timeOut, int minutes, int question_no, int seconds)
        {
            string? studentId = getUserId();
            if (studentId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            HttpContext.Session.SetInt32(question_no.ToString(), answer == null ? 0 : answer.Value);
            // if finish exam
            if ((button != null && button.Equals("Finish")) || timeOut == 1)
            {
                ResponseBase<List<Quiz>?> response = _service.Finish(id);
                if (response.Data == null)
                {
                    return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
                }
                int score = 0;
                for (int i = 0; i < response.Data.Count; i++)
                {
                    int QuestionNo = i + 1;
                    // get answer of question
                    int? ans = HttpContext.Session.GetInt32(QuestionNo.ToString());
                    // if choose correct answer
                    if (ans == response.Data[i].AnswerCorrect)
                    {
                        score++;
                    }
                    HttpContext.Session.Remove(QuestionNo.ToString());
                }
                float FinalScore = (float)score / response.Data.Count * 10;
                string status = FinalScore >= 5.0 ? ResultStatus.Passed.getDescription() : ResultStatus.Not_Passed.getDescription();
                ResponseBase<Result?> responseDTO = _service.Finish(id, Guid.Parse(studentId), FinalScore, status);
                if (responseDTO.Data == null)
                {
                    return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
                }
                return View("/Views/TakeQuiz/Result.cshtml", responseDTO.Data);
            }
            ResponseBase<Dictionary<string, object?>?> responseDic = _service.Index(id, Guid.Parse(studentId), button, minutes, question_no, seconds);
            if (responseDic.Data == null)
            {
                return View("/Views/Error/" + responseDic.Code + ".cshtml", new ResponseBase<object?>(responseDic.Message, responseDic.Code));
            }
            return View(responseDic.Data);
        }
    }
}
