using Common.Base;
using Common.Const;
using Common.DTO.CourseDTO;
using Common.Entity;
using Common.Paginations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ManagerCourse;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    [ResponseCache(NoStore = true)]
    public class ManagerCourseController : BaseController
    {
        private readonly IManagerCourseService _service;

        public ManagerCourseController(IManagerCourseService service)
        {
            _service = service;
        }

        public ActionResult Index(int? page)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Pagination<Course>?> response = _service.Index(page, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            return View(response.Data);
        }
        
        public ActionResult Create()
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ResponseBase<List<Category>?> response = _service.Create();
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message, response.Code));
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Create(CourseCreateUpdateDTO DTO)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<List<Category>?> response = _service.Create(DTO, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
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

        public ActionResult Update(Guid? id)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            if (id == null)
            {
                return Redirect("/ManagerCourse");
            }
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Update(id.Value, Guid.Parse(teacherId));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            return View(response.Data);
        }

        [HttpPost]
        public ActionResult Update(Guid id, CourseCreateUpdateDTO DTO)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            ResponseBase<Dictionary<string, object>?> response = _service.Update(id, DTO);
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseBase<object?>(response.Message));
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

        public ActionResult Delete(Guid? id)
        {
            if (isLogin == false)
            {
                return Redirect("/Home");
            }
            if (id == null)
            {
                return Redirect("/Home");
            }
            string? teacherId = getUserId();
            if (teacherId == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found Id. Please check login information"));
            }
            ResponseBase<bool> response = _service.Delete(id.Value, Guid.Parse(teacherId));
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/Home");
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            return Redirect("/ManagerCourse");
        }
    }
}
