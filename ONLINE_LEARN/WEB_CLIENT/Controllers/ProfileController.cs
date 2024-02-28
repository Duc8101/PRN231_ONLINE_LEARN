using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly ProfileService service = new ProfileService();

        public async Task<ActionResult> Index()
        {
            // if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_ADMIN)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
            }
            string? UserID = getUserID();
            if (UserID == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, object>?> response = await service.Index(Guid.Parse(UserID));
            // if get result failed
            if (response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileDTO DTO, string valueImg)
        {
            string? role = getRole();
            if (role == null || role == UserConst.ROLE_ADMIN)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
            }
            string? UserID = getUserID();
            if (UserID == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, object>?> response = await service.Index(Guid.Parse(UserID), DTO, valueImg);
            // if get data failed
            if (response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }
    }
}
