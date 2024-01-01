using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT.Controllers
{
    public class ChangePasswordController : BaseController
    {
        private readonly ChangePasswordService service = new ChangePasswordService();

        public ActionResult Index()
        {
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_ADMIN)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ChangePasswordDTO DTO)
        {
            string? username = getUsername();
            // if not found username
            if (username == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found username. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<bool> response = await service.Index(username, DTO);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
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
