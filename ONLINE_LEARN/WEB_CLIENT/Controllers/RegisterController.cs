using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly RegisterService service = new RegisterService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(User user)
        {
            ResponseDTO<bool> response = await service.Index(user);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
                {
                    ViewData["message"] = response.Message;
                    return View();
                }
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            ViewData["mess"] = response.Message;
            return View();
        }
    }
}
