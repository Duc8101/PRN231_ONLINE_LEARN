using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class TakeQuizController : BaseController
    {
        private readonly ITakeQuizService service;
        public TakeQuizController(ITakeQuizService service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index(Guid? id /*LessonID*/)
        {
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_STUDENT)
            {
                string? StudentID = getUserID();
                if (StudentID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                if (id == null)
                {
                    return Redirect("/MyCourse");
                }
                ResponseDTO<Dictionary<string, object?>?> response = await service.Index(id.Value, Guid.Parse(StudentID));
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Index(Guid? id /*LessonID*/, string? button, int? answer, int timeOut, int minutes, int question_no, int seconds)
        {
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_STUDENT)
            {
                string? StudentID = getUserID();
                if (StudentID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                if (id == null)
                {
                    return Redirect("/MyCourse");
                }
                HttpContext.Session.SetInt32(question_no.ToString(), answer == null ? 0 : answer.Value);
                // if finish exam
                if ((button != null && button.Equals("Finish")) || timeOut == 1)
                {
                    ResponseDTO<List<Quiz>?> response = await service.Finish(id.Value);
                    if (response.Data == null)
                    {
                        return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
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
                    string status = FinalScore >= 5.0 ? ResultConst.STATUS_PASSED : ResultConst.STATUS_NOT_PASSED;
                    ResponseDTO<Result?> responseDTO = await service.Finish(id.Value, Guid.Parse(StudentID), FinalScore, status);
                    if (responseDTO.Data == null)
                    {
                        return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                    }
                    return View("/Views/TakeQuiz/Result.cshtml", responseDTO.Data);
                }
                ResponseDTO<Dictionary<string, object?>?> responseDic = await service.Index(id.Value, Guid.Parse(StudentID), button, minutes, question_no, seconds);
                if (responseDic.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, responseDic.Message, responseDic.Code));
                }
                return View(responseDic.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}
