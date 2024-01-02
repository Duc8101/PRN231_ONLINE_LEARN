using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT.Controllers
{
    public class ManagerCourseController : BaseController
    {
        private readonly ManagerCourseService service = new ManagerCourseService();

        public async Task<ActionResult> Index(int? page)
        {
            string? role = getRole();
            if(role == UserConst.ROLE_TEACHER)
            {
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<PagedResultDTO<Course>?> response = await service.Index(page, Guid.Parse(TeacherID));
                if(response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
        public async Task<ActionResult> Create()
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                ResponseDTO<List<Category>?> response = await service.Create();
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<List<Category>?> response = await service.Create(course, Guid.Parse(TeacherID));
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                if(response.Code == (int) HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                }
                else
                {
                    ViewData["success"] = response.Message;
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
        public async Task<ActionResult> Update(Guid? id)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                if(id == null)
                {
                    return Redirect("/ManagerCourse");
                }
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Update(id.Value, Guid.Parse(TeacherID));
                if (response.Data == null)
                {
                    if(response.Code == (int) HttpStatusCode.NotFound)
                    {
                        return Redirect("/ManagerCourse");
                    }
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Update(Guid id, Course course)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                ResponseDTO<Dictionary<string, object>?> response = await service.Update(id, course);
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                if (response.Code == (int) HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                }
                else
                {
                    ViewData["success"] = response.Message;
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                if (id == null)
                {
                    return Redirect("/Home");
                }
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                ResponseDTO<PagedResultDTO<Course>?> response = await service.Delete(id.Value, Guid.Parse(TeacherID));
                if (response.Data == null)
                {
                    if(response.Code == (int)HttpStatusCode.NotFound)
                    {
                        return Redirect("/Home");
                    }
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return Redirect("/ManagerCourse");
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}
