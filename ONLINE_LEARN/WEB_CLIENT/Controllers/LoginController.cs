using Common.Base;
using Common.Const;
using Common.DTO.UserDTO;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.Login;

namespace WEB_CLIENT.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            string? userId = Request.Cookies["userId"];
            if (userId == null)
            {
                return View();
            }
            ResponseBase<User?> response = _service.Index(Guid.Parse(userId));
            // if get user failed
            if (response.Data == null)
            {
                return Redirect("/Logout");
            }
            isLogin = true;
            HttpContext.Session.SetString("userId", userId);
            HttpContext.Session.SetString("username", response.Data.Username);
            HttpContext.Session.SetString("role", response.Data.RoleName);
            HttpContext.Session.SetString("image", response.Data.Image);
            return Redirect("/Home");
        }

        [HttpPost]
        public ActionResult Index(LoginDTO DTO)
        {
            ResponseBase<User?> response = _service.Index(DTO);
            // if get user failed
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseBase<object?>(null, response.Message, response.Code));
                }
                ViewData["message"] = response.Message;
                return View();
            }
            isLogin = true;
            HttpContext.Session.SetString("userId", response.Data.Id.ToString());
            HttpContext.Session.SetString("username", response.Data.Username);
            HttpContext.Session.SetString("role", response.Data.RoleName);
            HttpContext.Session.SetString("image", response.Data.Image);
            CookieOptions option = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(7)
            };
            // add cookie
            Response.Cookies.Append("userId", response.Data.Id.ToString(), option);
            if (response.Data.RoleName == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            return Redirect("/Home");
        }
    }
}
