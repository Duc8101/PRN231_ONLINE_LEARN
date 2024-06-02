using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    public class ManagerQuizController : BaseController
    {
        private readonly IManagerQuizService _service;
        public ManagerQuizController(IManagerQuizService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index(Guid? LessonID, Guid? CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            if (LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Index(LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Shared/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            return View(response.Data);
        }
        public async Task<ActionResult> Detail(Guid? id, Guid? LessonID, Guid? CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null || LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Detail(id.Value, LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            return View(response.Data);
        }
        public async Task<ActionResult> Create(Guid? LessonID, Guid? CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information"));
            }
            if (LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseDTO<Dictionary<string, Guid>?> response = await _service.Create(LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message));
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Quiz create, Guid LessonID, Guid CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, Guid>?> response = await _service.Create(create, LessonID, CourseID, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
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
        public async Task<ActionResult> Update(Guid? id, Guid? LessonID, Guid? CourseID)
        {
            return await Detail(id, LessonID, CourseID);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Guid id, Quiz obj, Guid LessonID, Guid CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Update(id, obj, LessonID, CourseID, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                return View("/Views/Error/" + response.Code + ".cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
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
        public async Task<ActionResult> Delete(Guid? id, Guid? LessonID, Guid? CourseID)
        {
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null || LessonID == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseDTO<Dictionary<string, object>?> response = await _service.Delete(id.Value, LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (response.Data == null)
            {
                if (response.Code == (int)HttpStatusCode.NotFound)
                {
                    return Redirect("/ManagerCourse");
                }
                return View("/Views/Error/500.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
            }
            ViewData["success"] = response.Message;
            return View("/Views/ManagerQuiz/Index.cshtml", response.Data);
        }
    }
}
