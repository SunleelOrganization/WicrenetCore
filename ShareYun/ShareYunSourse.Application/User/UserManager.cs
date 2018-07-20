using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShareYunSourse.Application.Dto;
using ShareYunSourse.Application.Users.Dto;
using ShareYunSourse.Core.Domain.PageResultDto;
using ShareYunSourse.EFCore.UOW;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShareYunSourse.Application
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userIRepository;
        private readonly IRepository<YunSourse> _yunSourseIRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IRepository<User> userIRepository,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            IRepository<YunSourse> yunSourseIRepository)
        {
            _userIRepository = userIRepository;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _yunSourseIRepository = yunSourseIRepository;
        }

        public async Task<PageResultDto<User>> GetUsers(GetUserInput input)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var query = _userIRepository.GetAll()
                   .WhereIf(!string.IsNullOrEmpty(input.UserName), m => m.UserName.Contains(input.UserName))
                   .WhereIf(!string.IsNullOrEmpty(input.Email), m => m.Email.Contains(input.Email));
            var count = query.CountAsync();
            var result = query
                    .Skip(input.Offset)
                    .Take(input.Limit)
                    .ToListAsync();
            await Task.WhenAll(count, result);
            sw.Stop();
            var ss = sw.ElapsedMilliseconds;
            return new PageResultDto<User>(count.Result, result.Result);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = _userIRepository.GetAll().AsNoTracking().FirstOrDefault(m => m.Id == id);
            if (user == null) { return new User(); }
            return user;
        }

        public async Task<User> GetUserByUserNameAndPwd(string userName, string userPwd)
        {
            var user = _userIRepository.GetAll().AsNoTracking().FirstOrDefault(m => m.UserName == userName);
            if (user == null) { return null; }
            if (string.Equals(user.UserPwd, userPwd)) { return user; }
            return null;
        }
        public async Task<int?> GetCurrUserIdAsync()
        {
            var auth = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            if (auth.Succeeded)
            {
                var userCli = auth.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); ;
                if (userCli == null || string.IsNullOrEmpty(userCli.Value))
                {
                    return null;
                }
                return Convert.ToInt32(userCli.Value);
            }
            return null;
        }

        public async Task<User> GetCurrUser()
        {
            User user = null;
            var userId = await GetCurrUserIdAsync();
            if (userId != null)
            {
                user = _userIRepository.GetAll().AsNoTracking().FirstOrDefault(m => m.Id == userId.Value);
            }
            return user;
        }


        public async Task SignSession(User user)
        {
            var claimsPri = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }, CookieAuthenticationDefaults.AuthenticationScheme));

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPri, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(30))
            });
        }


        public async Task EditOrCreateUserById(UserDto input)
        {
            if (input.Id == 0)
            {
                await CreateUser(input);
            }
            else
            {
                await EditUserById(input);
            }

        }
        private async Task CreateUser(UserDto input)
        {
            var user = new User();
            user.UserName = input.UserName;
            user.UserPwd = "123123";
            user.Email = input.Email;
            user.Age = input.Age;
            user.CreationTime = DateTime.Now;
            await _userIRepository.InsertAsync(user);
            _unitOfWork.SaveChanges();
        }
        private async Task EditUserById(UserDto input)
        {
            var user = await _userIRepository.GetAll().FirstOrDefaultAsync(m => m.Id == input.Id);
            if (user == null) { throw new Exception("未找到该用户信息"); }
            user.Age = input.Age;
            user.UserName = input.UserName;
            user.Email = input.Email;
            _userIRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteUserById(int id)
        {
            var user = await _userIRepository.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) { throw new Exception("未找到该用户信息"); }
            _userIRepository.Delete(user);
            _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// 测试代码，事务操作
        /// </summary>
        /// <returns></returns>
        public async Task Test()
        {
            var user = new User()
            {
                UserName = "1111",
                UserPwd = "12",
                CreationTime = DateTime.Now,
            };
            var user1 = new User()
            {
                UserName = "11",
                UserPwd = "121",
                CreationTime = DateTime.Now,
            };
            try
            {
                _unitOfWork.BeginTransaction();//开启事务
                var userId = _userIRepository.InsertAndGetId(user);//获取UserId,此时已经提交到数据库
                await _userIRepository.InsertAsync(user1);
                await _unitOfWork.SaveChangesAsync();//提交变更

                _unitOfWork.CommitTransaction();//提交事务
            }
            catch (Exception e)
            {
                _unitOfWork.RollBackTransaction();//回滚事务            
            }


        }


        public async Task TestAsync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var query = _userIRepository.GetAll();
            var query1 = _yunSourseIRepository.GetAll();
            var list = await query.Select(m => new { m.Email, m.UserName }).ToListAsync();//异步查询User表中全部数据

            var count = await query1.ToListAsync();//异步查询User表中的数量
            sw.Stop();
            var s = sw.ElapsedMilliseconds;
        }

        public async Task InsertUsers()
        {
            for (int i = 1; i < 10; i++)
            {
                _userIRepository.Insert(new User { UserName = "sunleel" + i, UserPwd = "sunleel", Email = i + "838283226@qq.com" });
                _yunSourseIRepository.Insert(new YunSourse { Content = "sunleel" + i, CreationTime = DateTime.Now, Title = "sunleel" });
            }
            _unitOfWork.SaveChanges();
        }
    }
}
