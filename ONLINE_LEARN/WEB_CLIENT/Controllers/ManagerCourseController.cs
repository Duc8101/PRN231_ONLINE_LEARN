using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class ManagerCourseController : BaseController
    {
        private readonly IManagerCourseService service;
        public ManagerCourseController(IManagerCourseService service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index(int? page)
        {
            string? role = getRole();
            if(role != null && role == UserConst.ROLE_TEACHER)
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
    }
}
