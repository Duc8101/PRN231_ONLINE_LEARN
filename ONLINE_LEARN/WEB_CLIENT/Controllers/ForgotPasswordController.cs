using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        private readonly ForgotPasswordService service = new ForgotPasswordService();

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
            ResponseDTO<bool> response = await service.Index(email);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    ViewData["error"] = response.Message;
                    return View();
                }
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            ViewData["success"] = response.Message;
            return View();
        }
    }
}
