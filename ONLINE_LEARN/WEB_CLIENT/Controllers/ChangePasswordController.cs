using Common.Base;
using Common.Const;
using Common.DTO.UserDTO;
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
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ChangePasswordDTO DTO)
        {
            string? username = getUsername();
            // if not found username
            if (username == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found username. Please check login information"));
            }
            ResponseBase<bool> response = await _service.Index(username, DTO);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
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
