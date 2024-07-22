using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult Index()
        {
            string? role = getRole();
            if (role == Roles.Admin.ToString())
            {
                return Redirect("/Admin");
            }
            return View();
        }
    }
}
