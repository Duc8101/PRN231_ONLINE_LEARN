using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_CLIENT.Attributes
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");
            if (role == null)
            {
                context.Result = new ViewResult()
                {
                    ViewName = "/Views/Error/401.cshtml"
                };
            }
        }
    }
}
