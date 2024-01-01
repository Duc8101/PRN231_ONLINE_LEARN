using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class BaseController : Controller
    {
        protected string? getUserID()
        {
            return HttpContext.Session.GetString("UserID");
        }

        protected string? getUsername()
        {
            return HttpContext.Session.GetString("username");
        }

        protected string? getRole()
        {
            return HttpContext.Session.GetString("role");
        }

    }
}
