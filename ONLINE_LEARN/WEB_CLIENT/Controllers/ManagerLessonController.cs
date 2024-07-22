using Common.Base;
using Common.DTO.LessonDTO;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ManagerLesson;

namespace WEB_CLIENT.Controllers
{
    [Role(Roles.Teacher)]
    [Authorize]
    //[ResponseCache(NoStore = true)]
    public class ManagerLessonController : BaseController
    {
        private readonly IManagerLessonService _service;

        public ManagerLessonController(IManagerLessonService service)
        {
            _service = service;
        }
        public ActionResult Index(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            if (id == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Index(id.Value, Guid.Parse(teacherId), video, name, PDF, lessonId);
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
                }
                return Redirect("/ManagerCourse");
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Create(LessonCreateUpdateDTO DTO)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = _service.Create(DTO, Guid.Parse(teacherId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(result.Message));
            }
            if (result.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = result.Message;
            }
            else
            {
                ViewData["success"] = result.Message;
            }
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }

        [HttpPost]
        public ActionResult Update(Guid id, LessonCreateUpdateDTO DTO)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = _service.Update(id, DTO, Guid.Parse(teacherId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(result.Message, result.Code));
            }
            if (result.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = result.Message;
            }
            else
            {
                ViewData["success"] = result.Message;
            }
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }

        public ActionResult Delete(Guid? id, Guid? courseId)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null || courseId == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> result = _service.Delete(id.Value, courseId.Value, Guid.Parse(teacherId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(result.Message, result.Code));
            }
            ViewData["success"] = result.Message;
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }
    }
}
