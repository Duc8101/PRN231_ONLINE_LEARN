using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            string? UserID = Request.Cookies["UserID"];
            // if not set cookie or cookie expired
            if(UserID == null)
            {
                return View();
            }
            ResponseDTO<User?> response = await service.Index(Guid.Parse(UserID));
            // if get user failed
            if(response.Data == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            HttpContext.Session.SetString("UserID", UserID);
            HttpContext.Session.SetString("username", response.Data.Username);
            HttpContext.Session.SetString("role", response.Data.RoleName);
            HttpContext.Session.SetString("image", response.Data.Image);
            return Redirect("/Home");
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginDTO DTO)
        {
            ResponseDTO<User?> response = await service.Index(DTO);
            // if get user failed
            if (response.Data == null)
            {
                if(response.Code == (int) HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                ViewData["message"] = response.Message;
                return View();
            }
            HttpContext.Session.SetString("UserID", response.Data.Id.ToString());
            HttpContext.Session.SetString("username", response.Data.Username);
            HttpContext.Session.SetString("role", response.Data.RoleName);
            HttpContext.Session.SetString("image", response.Data.Image);
            CookieOptions option = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1)
            };
            // add cookie
            Response.Cookies.Append("UserID", response.Data.Id.ToString(), option);
            return Redirect("/Home");
        }
    }
}
