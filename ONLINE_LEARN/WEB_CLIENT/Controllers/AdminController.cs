using Common.Base;
using Common.Const;
using Common.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.Admin;

namespace WEB_CLIENT.Controllers
{
    [Authorize]
    [Role(UserConst.ROLE_ADMIN)]
    [ResponseCache(NoStore = true)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        public ActionResult Index(string? name)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Index(name);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }

        public ActionResult Detail(Guid? id)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            if (id == null)
            {
                return Redirect("/Admin");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Detail(id.Value);
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/Admin");
                }
            }
            return View(response.Data);
        }

        public ActionResult Create()
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            List<string> list = _service.Create();
            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateDTO DTO)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ResponseBase<List<string>?> response = await _service.Create(DTO);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            if (response.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = response.Message;
            }
            else
            {
                ViewData["success"] = response.Message;
            }
            return View(response.Data);
        }
    }
}
