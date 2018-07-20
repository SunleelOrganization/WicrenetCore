using Microsoft.AspNetCore.Mvc;
using ShareYunSourse.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareYunSourse.Web.Components
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly IUserManager _userManager;
        public HeaderViewComponent(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user=await  _userManager.GetCurrUser();
            return View(user);
        }
    }
}
