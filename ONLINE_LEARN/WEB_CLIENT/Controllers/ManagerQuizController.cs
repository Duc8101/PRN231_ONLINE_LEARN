using Common.Base;
using Common.Const;
using Common.DTO.QuizDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ManagerQuiz;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    //[ResponseCache(NoStore = true)]
    public class ManagerQuizController : BaseController
    {
        private readonly IManagerQuizService _service;
        public ManagerQuizController(IManagerQuizService service)
        {
            _service = service;
        }

        public ActionResult Index(Guid? lessonId, Guid? courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found Id. Please check login information"));
            }
            if (lessonId == null || courseId == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Index(lessonId.Value, courseId.Value, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Shared/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            return View(response.Data);
        }

        public ActionResult Detail(Guid? id, Guid? lessonId, Guid? courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found Id. Please check login information"));
            }
            if (id == null || lessonId == null || courseId == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Detail(id.Value, lessonId.Value, courseId.Value, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }

        public ActionResult Create(Guid? lessonId, Guid? courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found Id. Please check login information"));
            }
            if (lessonId == null || courseId == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, Guid>?> response = _service.Create(lessonId.Value, courseId.Value, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Create(QuizCreateUpdateDTO DTO, Guid courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, Guid>?> response = _service.Create(DTO, courseId, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }

        public ActionResult Update(Guid? id, Guid? lessonId, Guid? courseId)
        {
            return Detail(id, lessonId, courseId);
        }

        [HttpPost]
        public ActionResult Update(Guid id, QuizCreateUpdateDTO DTO, Guid courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Update(id, DTO, courseId, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }

        public ActionResult Delete(Guid? id, Guid? lessonId, Guid? courseId)
        {
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            if (id == null || lessonId == null || courseId == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Delete(id.Value, lessonId.Value, courseId.Value, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            ViewData["success"] = response.Message;
            return View("/Views/ManagerQuiz/Index.cshtml", response.Data);
        }
    }
}
