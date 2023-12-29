using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICoursesService service;
        public CoursesController(ICoursesService service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index(int? CategoryID, string? properties, string? flow, int? page)
        {
            string? role = getRole();
            if(role == null || role == UserConst.ROLE_STUDENT)
            {
                ResponseDTO<Dictionary<string, object>?> result = await service.Index(CategoryID, properties, flow, page, null);
                // if get result failed
                if (result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                return View(result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int) HttpStatusCode.Forbidden));
        }
        public async Task<ActionResult> Detail(Guid? id)
        {
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_STUDENT)
            {
                if (id == null)
                {
                    return Redirect("/Courses");
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Detail(id.Value);
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
        public async Task<ActionResult> EnrollCourse(Guid? id)
        {
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_STUDENT)
            {
                if (role == null)
                {
                    return Redirect("/Login");
                }
                string? StudentID = getUserID();
                if (StudentID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                if (id == null)
                {
                    return Redirect("/Courses");
                }
                ResponseDTO<Dictionary<string, object>?> result = await service.EnrollCourse(id.Value, Guid.Parse(StudentID));
                if (result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                ViewData["enroll"] = result.Message;
                return View("/Views/Courses/Index.cshtml", result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        public async Task<ActionResult> LearnCourse(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID, int? VideoID, int? PDFID)
        {
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_STUDENT)
            {
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
                ResponseDTO<Dictionary<string, object>?> result = await service.LearnCourse(id.Value, Guid.Parse(StudentID), video, name, PDF, LessonID, VideoID, PDFID);
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
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));

        }
    }
}
