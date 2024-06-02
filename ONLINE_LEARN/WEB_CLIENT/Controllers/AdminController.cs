using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Authorize]
    [Role(UserConst.ROLE_ADMIN)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index(string? name)
        {
            ResponseDTO<Dictionary<string, object>?> response = await _service.Index(name);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }

        public async Task<ActionResult> Detail(Guid? id)
        {
            if (id == null)
            {
                return Redirect("/Admin");
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Detail(id.Value);
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
            List<string> list = _service.Create();
            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            ResponseDTO<List<string>?> response = await _service.Create(user);
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
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
