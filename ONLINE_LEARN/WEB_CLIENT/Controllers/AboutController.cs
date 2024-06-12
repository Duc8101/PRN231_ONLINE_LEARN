using Common.Consts;
using Microsoft.AspNetCore.Mvc;

namespace WEB_CLIENT.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult Index()
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            return View();
        }
    }
}
