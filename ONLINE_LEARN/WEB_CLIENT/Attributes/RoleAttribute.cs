using Common.Const;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WEB_CLIENT.Providers;

namespace WEB_CLIENT.Attributes
{
    public class RoleAttribute : Attribute, IActionFilter
    {
        private string[] Role { get; set; }
        public RoleAttribute(params string[] role)
        {
            Role = role;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // không thể khởi tạo service thông qua contructor như bình thường 
            // phải tạo ra 1 cái là provider, đây là 1 cách để lấy service đã được khởi tạo ra
            var httpContext = StaticServiceProvider.Provider.GetService<IHttpContextAccessor>();
            if (httpContext == null || httpContext.HttpContext == null)
            {
                context.Result = new ViewResult()
                {
                    ViewName = "/Views/Error/Role.cshtml"
                };
            }
            else
            {
                string? role = httpContext.HttpContext.Session.GetString("role");
                if (role == null)
                {
                    role = UserConst.ROLE_NONE;
                }
                bool check = Role.Contains(role);
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
