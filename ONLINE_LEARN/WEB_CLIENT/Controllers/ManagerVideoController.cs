using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    public class ManagerVideoController : BaseController
    {
        private readonly IManagerVideoService _service;
        public ManagerVideoController(IManagerVideoService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> Create(LessonVideo create, Guid CourseID)
        {
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseDTO<Dictionary<string, object>?> result = await _service.Create(create, CourseID, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
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
        public async Task<ActionResult> Update(int id, LessonVideo obj, Guid CourseID)
        {
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, object>?> result = await _service.Update(id, obj, CourseID, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
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
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null || LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseDTO<Dictionary<string, object>?> result = await _service.Delete(id.Value, LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
            }
            ViewData["success"] = result.Message;
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }
    }
}
