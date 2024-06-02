using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class BaseController : Controller
    {
        internal static Guid? IDLogin = null;
        internal string? getUserID()
        {
            return HttpContext.Session.GetString("UserID");
        }

        internal string? getUsername()
        {
            return HttpContext.Session.GetString("username");
        }

        internal string? getRole()
        {
            return HttpContext.Session.GetString("role");
        }

    }
}
