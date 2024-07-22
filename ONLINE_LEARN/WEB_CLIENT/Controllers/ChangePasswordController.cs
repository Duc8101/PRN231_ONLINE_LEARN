﻿using Common.Base;
using Common.DTO.UserDTO;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WEB_CLIENT.Attributes;
using WEB_CLIENT.Services.ChangePassword;

namespace WEB_CLIENT.Controllers
{
    [Role(Roles.Student, Roles.Teacher)]
    [Authorize]
    //[ResponseCache(NoStore = true)]
    public class ChangePasswordController : BaseController
    {
        private readonly IChangePasswordService _service;

        public ChangePasswordController(IChangePasswordService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordDTO DTO)
        {
            string? username = getUsername();
            // if not found username
            if (username == null)
            {
                return View("/Views/Error/404.cshtml", new ResponseBase<object?>("Not found username. Please check login information"));
            }
            ResponseBase<bool> response = _service.Index(username, DTO);
            if (response.Data == false)
            {
                if (response.Code == (int)HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                    return View();
                }
                return View("/Views/Error/500.cshtml", new ResponseBase<object?>(response.Message));
            }
            ViewData["success"] = response.Message;
            return View();
        }
    }
}
