using Common.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        private readonly IForgotPasswordService _service;

        public ForgotPasswordController(IForgotPasswordService service)
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
        public async Task<ActionResult> Index(string email)
        {
            ResponseBase<bool> response = await _service.Index(email);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    ViewData["error"] = response.Message;
                    return View();
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, response.Message));
            }
            ViewData["success"] = response.Message;
            return View();
        }
    }
}
