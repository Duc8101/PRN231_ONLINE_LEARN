using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_STUDENT, UserConst.ROLE_TEACHER)]
    [Authorize]
    public class ChangePasswordController : BaseController
    {
        private readonly IChangePasswordService _service;

        public ChangePasswordController(IChangePasswordService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            /*// if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ChangePasswordDTO DTO)
        {
            /*// if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            string? username = getUsername();
            // if not found username
            if (username == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found username. Please check login information"));
            }
            ResponseDTO<bool> response = await _service.Index(username, DTO);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                    return View();
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            ViewData["success"] = response.Message;
            return View();
        }
    }
}
