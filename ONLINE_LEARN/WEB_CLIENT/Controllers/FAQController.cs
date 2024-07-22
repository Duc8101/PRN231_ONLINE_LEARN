using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class FAQController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["FAQ"] = true;
            string? role = getRole();
            if (role == Roles.Admin.ToString())
            {
                return Redirect("/Admin");
            }
            return View();
        }
    }
}
