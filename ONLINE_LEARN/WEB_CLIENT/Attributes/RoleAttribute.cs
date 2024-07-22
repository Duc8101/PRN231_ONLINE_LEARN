using Common.Enums;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WEB_CLIENT.Providers;

namespace WEB_CLIENT.Attributes
{
    public class RoleAttribute : Attribute, IActionFilter
    {
        public string[] Roles { get; }
        public RoleAttribute(params Roles[] roles)
        {
            Roles = Array.ConvertAll(roles, e => e.ToString());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // không thể khởi tạo service thông qua contructor như bình thường 
            // phải tạo ra 1 cái là provider, đây là 1 cách để lấy service đã được khởi tạo ra
            var accessor = StaticServiceProvider.Provider.GetRequiredService<IHttpContextAccessor>();
            if (accessor.HttpContext == null)
            {
                context.Result = new ViewResult()
                {
                    ViewName = "/Views/Error/Role.cshtml"
                };
            }
            else
            {
                string? role = accessor.HttpContext.Session.GetString("role");
                if (role == null)
                {
                    role = Common.Enums.Roles.None.getDescription();
                }
                bool check = Roles.Contains(role);
                if (check == false)
                {
                    context.Result = new ViewResult()
                    {
                        ViewName = "/Views/Error/403.cshtml"
                    };
                }
            }
        }
    }
}
