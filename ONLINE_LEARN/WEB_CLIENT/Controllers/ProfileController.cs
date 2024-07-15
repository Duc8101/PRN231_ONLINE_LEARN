using Common.Base;
using Common.Const;
using Common.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.Profile;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_STUDENT, UserConst.ROLE_TEACHER)]
    [Authorize]
    [ResponseCache(NoStore = true)]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            string? userId = getUserId();
            if (userId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found id. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Index(Guid.Parse(userId));
            // if get result failed
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Index(ProfileDTO DTO, string valueImg)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            string? userId = getUserId();
            if (userId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Index(Guid.Parse(userId), DTO, valueImg);
            // if get data failed
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message, response.Code));
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
