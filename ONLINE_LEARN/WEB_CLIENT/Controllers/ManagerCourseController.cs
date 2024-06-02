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
    public class ManagerCourseController : BaseController
    {
        private readonly IManagerCourseService _service;

        public ManagerCourseController(IManagerCourseService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(int? page)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseDTO<PagedResultDTO<Course>?> response = await _service.Index(page, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            return View(response.Data);
        }
        public async Task<ActionResult> Create()
        {
            ResponseDTO<List<Category>?> response = await _service.Create();
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseDTO<List<Category>?> response = await _service.Create(course, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }
        public async Task<ActionResult> Update(Guid? id)
        {
            if (id == null)
            {
                return Redirect("/ManagerCourse");
            }
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Update(id.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Guid id, Course course)
        {
            ResponseDTO<Dictionary<string, object>?> response = await _service.Update(id, course);
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return Redirect("/Home");
            }
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseDTO<PagedResultDTO<Course>?> response = await _service.Delete(id.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/Home");
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            return Redirect("/ManagerCourse");
        }
    }
}
