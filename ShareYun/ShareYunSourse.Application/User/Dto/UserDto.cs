using System;
using System.Collections.Generic;
using System.Text;

namespace ShareYunSourse.Application.Users.Dto
{
  public  class UserDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

    }
}
