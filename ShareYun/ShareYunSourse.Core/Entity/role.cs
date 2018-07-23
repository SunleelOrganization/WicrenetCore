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
    [Table("Role")]
    public class Role : Entity
    {
        public Role()
        {
            yunSourses = new HashSet<YunSourse>();
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(50)]
        public string RoleName { get; set; }

        [MaxLength(2)]
        public int status { get; set; }
        public DateTime CreationTime { get; set; }

        public int SourseID { get; set; }
        public virtual ICollection<YunSourse> yunSourses { get; set; }

    }
}
