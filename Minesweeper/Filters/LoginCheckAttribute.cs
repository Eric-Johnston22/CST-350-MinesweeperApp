using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Minesweeper.Filters
{
    public class LoginCheckAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Session.GetInt32("uid")==null)
            {
                context.Result = new BadRequestObjectResult("We know that you are not logged in.");
            }
        }
    }
}
