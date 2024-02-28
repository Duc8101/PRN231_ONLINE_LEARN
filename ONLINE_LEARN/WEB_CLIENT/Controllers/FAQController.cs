using DataAccess.Const;
using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class FAQController : BaseController
    {

        public ActionResult Index()
        {
            // if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }
            ViewData["FAQ"] = true;
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            return View();
        }
    }
}
