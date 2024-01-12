﻿using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WEB_CLIENT.Services;

namespace WEB_CLIENT.Controllers
{
    public class AdminController : BaseController
    {
        private readonly AdminService service = new AdminService();
        public async Task<ActionResult> Index(string? name)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                ResponseDTO<Dictionary<string, object>?> response = await service.Index(name);
                if (response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        public async Task<ActionResult> Detail(Guid? id)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                if(id == null)
                {
                    return Redirect("/Admin");
                }
                ResponseDTO<Dictionary<string, object>?> response = await service.Detail(id.Value);
                if(response.Data == null)
                {
                    if(response.Code == (int) HttpStatusCode.NotFound)
                    {
                        return Redirect("/Admin");
                    }     
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        public ActionResult Create()
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                List<string> list = service.Create();
                return View(list);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            string? role = getRole();
            if (role == UserConst.ROLE_ADMIN)
            {
                ResponseDTO<List<string>?> response = await service.Create(user);
                if(response.Data == null)
                {
                    return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, response.Message, response.Code));
                }
                if(response.Code == (int)HttpStatusCode.Conflict)
                {
                    ViewData["error"] = response.Message;
                }
                else
                {
                    ViewData["success"] = response.Message;
                }
                return View(response.Data);
            }
            return View("/Views/Shared/Error.cshtml", new ResponseDTO<object?>(null, "You are not allowed to access this page", (int)HttpStatusCode.Forbidden));
        }
    }
}