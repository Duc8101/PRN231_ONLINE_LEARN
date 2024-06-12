﻿using Common.Base;
using Common.Const;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Controllers
{
    [Role(UserConst.ROLE_TEACHER)]
    [Authorize]
    public class ManagerLessonController : BaseController
    {
        private readonly IManagerLessonService _service;

        public ManagerLessonController(IManagerLessonService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index(Guid? id, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID)
        {
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            if (id == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Index(id.Value, Guid.Parse(TeacherID), video, name, PDF, LessonID);
            if (result.Data == null)
            {
                if (result.Code == (int)HttpStatusCode.InternalServerError)
                {
                    return View("/Views/Error/500.cshtml", new ResponseBase<object?>(null, result.Message));
                }
                return Redirect("/ManagerCourse");
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<ActionResult> Create(string? LessonName, Guid CourseID)
        {
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Create(LessonName, CourseID, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message));
            }
            if (result.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = result.Message;
            }
            else
            {
                ViewData["success"] = result.Message;
            }
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Guid id, string? LessonName, Guid CourseID)
        {
            /*// if session time out
            if (isSessionTimeout())
            {
                return Redirect("/Logout");
            }*/
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information"));
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Update(id, LessonName, CourseID, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message, result.Code));
            }
            if (result.Code == (int)HttpStatusCode.Conflict)
            {
                ViewData["error"] = result.Message;
            }
            else
            {
                ViewData["success"] = result.Message;
            }
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }

        public async Task<ActionResult> Delete(Guid? id, Guid? CourseID)
        {
            ViewData["ViewLesson"] = true;
            string? TeacherID = getUserID();
            if (TeacherID == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>(null, "Not found ID. Please check login information", (int)HttpStatusCode.NotFound));
            }
            if (id == null || CourseID == null)
            {
                return Redirect("/ManagerCourse");
            }
            ResponseBase<Dictionary<string, object>?> result = await _service.Delete(id.Value, CourseID.Value, Guid.Parse(TeacherID));
            if (result.Data == null)
            {
                return View("/Views/Error/" + result.Code + ".cshtml", new ResponseBase<object?>(null, result.Message, result.Code));
            }
            ViewData["success"] = result.Message;
            return View("/Views/ManagerLesson/Index.cshtml", result.Data);
        }
    }
}
