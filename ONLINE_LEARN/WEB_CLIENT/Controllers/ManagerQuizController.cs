using DataAccess.Const;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class ManagerQuizController : BaseController
    {
        private readonly ManagerQuizService service = new ManagerQuizService();
        public async Task<ActionResult> Index(Guid? LessonID, Guid? CourseID)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                if (LessonID == null || CourseID == null)
                {
                    return Redirect("/ManagerCourse");
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Index(LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
                if (response.Data == null)
                {
                    if (response.Code == (int)HttpStatusCode.NotFound)
                    {
                        return Redirect("/ManagerCourse");
                    }
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        public async Task<ActionResult> Detail(Guid? id, Guid? LessonID, Guid? CourseID)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_TEACHER)
            {
                string? TeacherID = getUserID();
                if (TeacherID == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
                }
                if (id == null || LessonID == null || CourseID == null)
                {
                    return Redirect("/ManagerCourse");
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Update(id.Value, LessonID.Value, CourseID.Value, Guid.Parse(TeacherID));
                if (response.Data == null)
                {
                    if (response.Code == (int)HttpStatusCode.NotFound)
                    {
                        return Redirect("/ManagerCourse");
                    }
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}
