using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class LogoutController : BaseController
    {
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            /* string? UserID = Request.Cookies["UserID"];
             CookieOptions option = new CookieOptions
             {
                 Expires = DateTime.Now.AddDays(-1)
             };
             if (UserID != null)
             {
                 Response.Cookies.Append("UserID", UserID, option);
             }*/
            IDLogin = null;
            return Redirect("/Home");
        }
    }
}
