using Common.Base;
using Common.Consts;
using Common.Entity;
using Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_STUDENT)]
    [Authorize]
    public class MyCourseController : BaseController
    {
        private readonly IMyCourseService _service;

        public MyCourseController(IMyCourseService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(int? page)
        {
            string? StudentID = getUserID();
            if (StudentID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            int pageSelected = page == null ? 1 : page.Value;
            ResponseBase<PagedResult<Course>?> response = await _service.Index(Guid.Parse(StudentID), pageSelected);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }
    }
}
