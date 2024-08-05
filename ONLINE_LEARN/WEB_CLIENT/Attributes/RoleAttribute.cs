using Common.Enums;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_CLIENT.Attributes
{
    public class RoleAttribute : Attribute, IActionFilter
    {
        public string[] Roles { get; }
        public RoleAttribute(params Roles[] roles)
        {
            Roles = Array.ConvertAll(roles, e => e.getDescription());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");
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
