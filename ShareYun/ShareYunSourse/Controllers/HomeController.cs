using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShareYunSourse.Models;
using ShareYunSourse.Application;
using ShareYunSourse.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using ShareYunSourse.Application.Dto;
using ShareYunSourse.Web.Filter;
using System;
using ShareYunSourse.Application.Users.Dto;

namespace ShareYunSourse.Controllers
{
    [UserFilter]
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        public HomeController(IUserManager  userManager)
        {
            _userManager = userManager;
        }
     
        public async Task<IActionResult> Index()
        {
            return View();
        }
   
        public async Task<JsonResult> GetUsersAsync(GetUserInput input)
        {
            var users= await _userManager.GetUsers(input);
            return Json(users); 
        }
        [HttpGet]
        public async Task<IActionResult> ViewUser(int id)
        {
            var users = await _userManager.GetUserById(id);
            return Json(users);
        }

        public async Task  EditOrCreateUser(UserDto input)
        {         
            await _userManager.EditOrCreateUserById(input);         
        }
        public async Task DeleteUserById(int id)
        {
            await _userManager.DeleteUserById(id);
        }

        public async Task<IActionResult> Test()
        {
            await _userManager.TestAsync();
            return Json("s");
        }
    }
}
