using Common.Base;
using Common.Const;
using Common.Entity;
using Common.Paginations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.MyCourse;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_STUDENT)]
    [Authorize]
    //[ResponseCache(NoStore = true)]
    public class MyCourseController : BaseController
    {
        private readonly IMyCourseService _service;

        public MyCourseController(IMyCourseService service)
        {
            _service = service;
        }

        public ActionResult Index(int? page)
        {
            string? studentId = getUserId();
            if (studentId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            int pageSelected = page == null ? 1 : page.Value;
            ResponseBase<Pagination<Course>?> response = _service.Index(Guid.Parse(studentId), pageSelected);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }
    }
}
