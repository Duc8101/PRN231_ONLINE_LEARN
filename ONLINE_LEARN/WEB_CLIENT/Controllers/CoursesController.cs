using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index(int? CategoryID, string? properties, string? flow, int? page)
        {
            return View();
        }
    }
}
