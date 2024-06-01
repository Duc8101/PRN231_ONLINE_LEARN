using DataAccess.Const;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICoursesService _service;
        public CoursesController(ICoursesService service)
        {
            _service = service;
        }

        [Role(UserConst.ROLE_NONE, UserConst.ROLE_STUDENT)]
        public async Task<ActionResult> Index(int? CategoryID, string? properties, string? flow, int? page)
        {
           /* // if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            string? userId = getUserID();
            ResponseDTO<Dictionary<string, object?>?> result = await _service.Index(CategoryID, properties, flow, page, userId);
            // if get result failed
            if (result.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
            }
            return View(result.Data);
        }
        [Role(UserConst.ROLE_NONE, UserConst.ROLE_STUDENT)]
        public async Task<ActionResult> Detail(Guid? id)
        {
            /*// if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            string? userId = getUserID();
            if (id == null)
            {
                return Redirect("/Courses");
            }
            ResponseDTO<Dictionary<string, object?>?> response = await _service.Detail(id.Value, userId);
            if (response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }
        [Role(UserConst.ROLE_NONE, UserConst.ROLE_STUDENT)]
        public async Task<ActionResult> EnrollCourse(Guid? id)
        {
/*            // if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            string? role = getRole();
            if (role == null)
            {
                return Redirect("/Login");
            }
            string? StudentID = getUserID();
            if (StudentID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            if (id == null)
            {
                return Redirect("/Courses");
            }
            ResponseDTO<Dictionary<string, object?>?> result = await _service.EnrollCourse(id.Value, Guid.Parse(StudentID));
            if (result.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
            }
            ViewData["enroll"] = result.Message;
            return View("/Views/Courses/Index.cshtml", result.Data);
        }
        [Role(UserConst.ROLE_STUDENT)]
        [Authorize]
        public async Task<ActionResult> LearnCourse(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID, int? VideoID, int? PDFID)
        {
            /*// if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            ViewData["LearnCourse"] = true;
            if (id == null)
            {
                return Redirect("/Courses");
            }
            string? StudentID = getUserID();
            if (StudentID == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, object>?> result = await _service.LearnCourse(id.Value, Guid.Parse(StudentID), video, name, PDF, LessonID, VideoID, PDFID);
            if (result.Data == null)
            {
                if (result.Code == (int)HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                return Redirect("/Courses");
            }
            result.Data["CourseID"] = id;
            return View(result.Data);

        }
    }
}
