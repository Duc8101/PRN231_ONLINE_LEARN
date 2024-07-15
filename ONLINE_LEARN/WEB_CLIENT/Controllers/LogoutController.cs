using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class LogoutController : BaseController
    {
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("userId");
            return Redirect("/Home");
        }
    }
}
