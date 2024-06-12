using Common.Consts;
using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class FAQController : BaseController
    {
        public ActionResult Index()
        {
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
