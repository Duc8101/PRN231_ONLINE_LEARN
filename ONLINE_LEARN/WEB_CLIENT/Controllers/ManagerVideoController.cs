using Common.Base;
using Common.Const;
using Common.DTO.LessonVideoDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ManagerVideo;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    //[ResponseCache(NoStore = true)]
    public class ManagerVideoController : BaseController
    {
        private readonly IManagerVideoService _service;
        public ManagerVideoController(IManagerVideoService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create(LessonVideoCreateUpdateDTO DTO, Guid CourseId)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Create(DTO, CourseId, Guid.Parse(teacherId));
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
            return View("/Views/ManagerLesson/Index.cshtml", response.Data);
        }

        [HttpPost]
        public ActionResult Update(int id, LessonVideoCreateUpdateDTO DTO, Guid CourseId)
        {
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Update(id, DTO, CourseId, Guid.Parse(teacherId));
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
            return View("/Views/ManagerLesson/Index.cshtml", response.Data);
        }

        public ActionResult Delete(int? id, Guid? lessonId, Guid? courseId)
        {
            ViewData["ViewLesson"] = true;
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
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            ViewData["success"] = response.Message;
            return View("/Views/ManagerLesson/Index.cshtml", response.Data);
        }
    }
}
