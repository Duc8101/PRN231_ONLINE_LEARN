using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class BaseController : Controller
    {
        internal bool isLogin = false;
        internal string? getUserId()
        {
            return HttpContext.Session.GetString("userId");
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
