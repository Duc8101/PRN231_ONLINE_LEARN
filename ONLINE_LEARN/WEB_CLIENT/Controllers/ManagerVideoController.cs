using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class ManagerVideoController : BaseController
    {
        private readonly ManagerVideoService service = new ManagerVideoService();
        [HttpPost]
        public async Task<ActionResult> Create(LessonVideo create, Guid CourseID)
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
                ResponseDTO<Dictionary<string, object>?> result = await service.Create(create, CourseID, Guid.Parse(TeacherID));
                if (result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
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
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}
