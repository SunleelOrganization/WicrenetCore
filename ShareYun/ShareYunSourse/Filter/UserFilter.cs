using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShareYunSourse.Web.Filter
{
    public class UserFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auth = await context.HttpContext.AuthenticateAsync();
            if (auth.Succeeded)
            {
                var userCli = auth.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); ;
                if (userCli == null || string.IsNullOrEmpty(userCli.Value))
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
                else { await next(); }

            }
            else { context.Result = new RedirectResult("/Account/Login"); }
          
        }
    }
}
