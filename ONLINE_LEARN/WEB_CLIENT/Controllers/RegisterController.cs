using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IRegisterService _service;

        public RegisterController(IRegisterService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            string? role = getRole();
            if (role == null)
            {
                return View();
            }
            return Redirect("/Home");
        }

        [HttpPost]
        public async Task<ActionResult> Index(User user)
        {
            ResponseDTO<bool> response = await _service.Index(user);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                    return View();
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            ViewData["success"] = response.Message;
            return View();
        }
    }
}
