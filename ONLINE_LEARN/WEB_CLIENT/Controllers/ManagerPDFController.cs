using Common.Base;
using Common.Const;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

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
        public async Task<ActionResult> Create(LessonPdf create, Guid CourseID)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Create(create, CourseID, Guid.Parse(TeacherID));
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
        public async Task<ActionResult> Update(int id, LessonPdf obj, Guid CourseID)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Update(id, obj, CourseID, Guid.Parse(TeacherID));
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

        public async Task<ActionResult> Delete(int? id, Guid? LessonID, Guid? CourseID)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            if (id == null || LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Delete(id.Value, LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message, result.Code));
            }
            ViewData["success"] = result.Message;
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }
    }
}
