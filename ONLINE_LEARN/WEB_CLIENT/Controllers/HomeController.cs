using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeService service;
        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/ManagerUser");
            }
            string? username = getUserName();
            // if not found username
            if (role != null && username == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found username. Please check login information"));
            }
            ResponseDTO<List<User>?> response = await service.Index();
            // if get list failed
            if(response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", response);
            }
            Dictionary<string, object?> result = new Dictionary<string, object?>();
            result["username"] = username;
            result["role"] = role;
            result["list"] = response.Data;
            return View(result);
        }

    }
}