using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class MyCourseController : BaseController
    {
        private readonly MyCourseService service = new MyCourseService();

        public async Task<ActionResult> Index(int? page)
        {
            // if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }
            string? role = getRole();
            if (role != UserConst.ROLE_STUDENT)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
            }
            string? StudentID = getUserID();
            if (StudentID == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            int pageSelected = page == null ? 1 : page.Value;
            ResponseDTO<PagedResultDTO<Course>?> response = await service.Index(Guid.Parse(StudentID), pageSelected);
            if (response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }
    }
}
