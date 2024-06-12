using Common.Base;
using Common.Consts;
using Common.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_STUDENT, UserConst.ROLE_TEACHER)]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index()
        {
            string? UserID = getUserID();
            if (UserID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseBase<Dictionary<string, object>?> response = await _service.Index(Guid.Parse(UserID));
            // if get result failed
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileDTO DTO, string valueImg)
        {
            string? UserID = getUserID();
            if (UserID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = await _service.Index(Guid.Parse(UserID), DTO, valueImg);
            // if get data failed
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
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
