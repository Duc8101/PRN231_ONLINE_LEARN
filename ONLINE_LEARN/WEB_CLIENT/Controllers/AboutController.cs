using DataAccess.Const;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WEB_CLIENT.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult Index()
        {
            string? role = getRole();
            if (role != null && role == UserConst.ROLE_ADMIN)
            {
                return Redirect("/Admin");
            }
            string? username = getUserName();
            // if not found username
            if (role != null && username == null)
            {
                return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found username. Please check login information", (int)HttpStatusCode.NotFound));
            }
            return View();
        }
    }
}
