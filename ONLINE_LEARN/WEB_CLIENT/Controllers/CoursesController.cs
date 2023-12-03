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
                ResponseDTO<Dictionary<string, object>?> result = await service.Index(CategoryID, properties, flow, page);
                // if get result failed
                if(result.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, result.Message, result.Code));
                }
                return View(result.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null,"You are not allowed to access this page", (int) HttpStatusCode.Forbidden));
        }
        public async Task<ActionResult> Detail(Guid? id)
        {
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_STUDENT)
            {
                if(id == null)
                {
                    return Redirect("/Courses");
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Detail(id.Value);
                if(response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int) HttpStatusCode.Forbidden));
        }
    }
}
