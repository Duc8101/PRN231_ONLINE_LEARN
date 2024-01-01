using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HomeService service = new HomeService();
        public async Task<ActionResult> Index()
        {
            string? role = getRole();
            if(role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            // get top 4 teacher
            ResponseDTO<List<User>?> response = await service.Index();
            // if get list failed
            if (response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

    }
}