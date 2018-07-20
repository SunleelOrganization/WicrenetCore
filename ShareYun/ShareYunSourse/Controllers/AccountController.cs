using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareYunSourse.Web.Models.Account;
using ShareYunSourse.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;

namespace ShareYunSourse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserByUserNameAndPwd(input.UserName, input.UserPwd);
                 if (user == null) { return Json(new LoginResultModel { Error = "用户名或密码错误" }); }
                await _userManager.SignSession(user);
                return Json(new LoginResultModel { SuccInfo = "登录成功",ReturnUrl="/Home/Index" });
            }
            else { return Json(new LoginResultModel { Error = "用户名或密码错误" }); }
        }
    }
}