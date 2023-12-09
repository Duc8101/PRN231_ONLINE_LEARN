using DataAccess.Const;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WEB_CLIENT.Controllers
{
    public class FAQController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["FAQ"] = true;
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            string? username = getUsername();
            // if not found username
            if (role != null && username == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found username. Please check login information", (int)HttpStatusCode.NotFound));
            }
            return View();
        }
    }
}
