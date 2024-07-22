using Common.Base;
using Common.Entity;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using WEB_CLIENT.Services.Home;

namespace WEB_CLIENT.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeService _service;
        public HomeController(IHomeService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            string? role = getRole();
            if (role == Roles.Admin.ToString())
            {
                return Redirect("/Admin");
            }
            // get top 4 teacher
            ResponseBase<List<User>?> response = _service.Index();
            // if get list failed
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

    }
}