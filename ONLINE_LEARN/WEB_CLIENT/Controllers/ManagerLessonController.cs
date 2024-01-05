using DataAccess.Const;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class ManagerLessonController : BaseController
    {
        private readonly ManagerLessonService service = new ManagerLessonService();
        public async Task<ActionResult> Index(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                ViewData["ViewLesson"] = true;
                if (id == null)
                {
                    return Redirect("/ManagerCourse");
                }
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<Dictionary<string, object>?> result = await service.Index(id.Value, Guid.Parse(TeacherID), video, name, PDF, LessonID);
                if (result.Data == null)
                {
                    if (result.Code == (int)HttpStatusCode.InternalServerError)
                    {
                        return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                    }
                    return Redirect("/ManagerCourse");
                }
                return View(result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Create(string? LessonName, Guid CourseID)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                ViewData["ViewLesson"] = true;
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<Dictionary<string, object>?> result = await service.Create(LessonName, CourseID, Guid.Parse(TeacherID));
                if (result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                if(result.Code == (int) HttpStatusCode.Conflict)
                {
                    ViewData["error"] = result.Message;
                }
                else
                {
                    ViewData["success"] = result.Message;
                }
                return View("/Views/ManagerLesson/Index.cshtml", result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Update(Guid id, string? LessonName, Guid CourseID)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                ViewData["ViewLesson"] = true;
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<Dictionary<string, object>?> result = await service.Update(id, LessonName, CourseID, Guid.Parse(TeacherID));
                if (result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                if (result.Code == (int) HttpStatusCode.Conflict)
                {
                    ViewData["error"] = result.Message;
                }
                else
                {
                    ViewData["success"] = result.Message;
                }
                return View("/Views/ManagerLesson/Index.cshtml", result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}
