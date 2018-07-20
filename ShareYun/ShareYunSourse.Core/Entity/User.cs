using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareYunSourse
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("User")]
    public class User: Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(200)]
        public string UserPwd { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
