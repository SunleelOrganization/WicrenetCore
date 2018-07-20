using ShareYunSourse.Application.Dto;
using ShareYunSourse.Application.Users.Dto;
using ShareYunSourse.Core.Dependency;
using ShareYunSourse.Core.Domain.PageResultDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareYunSourse.Application
{
    public interface IUserManager : IDependency
    {

        Task<PageResultDto<User>> GetUsers(GetUserInput input);

        Task<User> GetUserById(int id);
        /// <summary>
        /// 根据用户名和密码 获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        Task<User> GetUserByUserNameAndPwd(string userName, string userPwd);

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        Task<User> GetCurrUser();
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task SignSession(User user);

        /// <summary>
        /// 编辑或创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task EditOrCreateUserById(UserDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteUserById(int id);

        Task Test();

        Task TestAsync();

        Task InsertUsers();
    }
}
