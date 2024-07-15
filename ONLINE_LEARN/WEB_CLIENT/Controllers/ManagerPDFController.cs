using Common.Base;
using Common.Const;
using Common.DTO.LessonPdfDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ManagerPDF;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    [ResponseCache(NoStore = true)]
    public class ManagerPDFController : BaseController
    {
        private readonly IManagerPDFService _service;
        public ManagerPDFController(IManagerPDFService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create(LessonPdfCreateUpdateDTO DTO, Guid CourseId)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = _service.Create(DTO, CourseId, Guid.Parse(teacherId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message, result.Code));
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
        public ActionResult Update(int id, LessonPdfCreateUpdateDTO DTO, Guid CourseId)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ViewData["ViewLesson"] = true;
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseBase<Dictionary<string, object>?> result = _service.Update(id, DTO, CourseId, Guid.Parse(teacherId));
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

        public ActionResult Delete(int? id, Guid? lessonId, Guid? courseId)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
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
            ResponseBase<Dictionary<string, object>?> result = _service.Delete(id.Value, lessonId.Value, courseId.Value, Guid.Parse(teacherId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(result.Message, result.Code));
            }
            ViewData["success"] = result.Message;
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }
    }
}
