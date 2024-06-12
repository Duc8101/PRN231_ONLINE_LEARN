using Common.Base;
using Common.Consts;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeService _service;
        public HomeController(IHomeService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index()
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            // get top 4 teacher
            ResponseBase<List<User>?> response = await _service.Index();
            // if get list failed
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

    }
}