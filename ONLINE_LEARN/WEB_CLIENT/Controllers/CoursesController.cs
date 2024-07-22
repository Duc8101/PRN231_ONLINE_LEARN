using Common.Base;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.Courses;

namespace WEB_CLIENT.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly ICoursesService _service;
        public CoursesController(ICoursesService service)
        {
            _service = service;
        }

        [Role(Roles.None, Roles.Student)]
        public ActionResult Index(int? categoryId, string? properties, bool? asc, int? page)
        {
            string? userId = getUserId();
            ResponseBase<Dictionary<string, object?>?> result = _service.Index(categoryId, properties, asc, page, userId);
            // if get result failed
            if (result.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(result.Message, result.Code));
            }
            return View(result.Data);
        }

        [Role(Roles.None, Roles.Student)]
        public ActionResult Detail(Guid? id)
        {
            string? userId = getUserId();
            if (id == null)
            {
                return Redirect("/Courses");
            }
            ResponseBase<Dictionary<string, object?>?> response = _service.Detail(id.Value, userId);
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

        [Role(Roles.None, Roles.Student)]
        public ActionResult EnrollCourse(Guid? id)
        {
            string? role = getRole();
            if (role == null)
            {
                return Redirect("/Login");
            }
            string? studentId = getUserId();
            if (studentId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            if (id == null)
            {
                return Redirect("/Courses");
            }
            ResponseBase<Dictionary<string, object?>?> result = _service.EnrollCourse(id.Value, Guid.Parse(studentId));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message, result.Code));
            }
            ViewData["enroll"] = result.Message;
            return View("/Views/Courses/Index.cshtml", result.Data);
        }

        [Role(Roles.Student)]
        [Authorize]
        //[ResponseCache(NoStore = true)]
        public ActionResult LearnCourse(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId, int? videoId, int? PDFId)
        {
            ViewData["LearnCourse"] = true;
            if (id == null)
            {
                return Redirect("/Courses");
            }
            string? studentId = getUserId();
            if (studentId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseBase<Dictionary<string, object>?> result = _service.LearnCourse(id.Value, Guid.Parse(studentId), video, name, PDF, lessonId, videoId, PDFId);
            if (result.Data == null)
            {
                if (result.Code == (int)HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Error/500.cshtml", new ResponseBase<object?>(result.Message, result.Code));
                }
                return Redirect("/Courses");
            }
            result.Data["courseId"] = id;
            return View(result.Data);

        }
    }
}
