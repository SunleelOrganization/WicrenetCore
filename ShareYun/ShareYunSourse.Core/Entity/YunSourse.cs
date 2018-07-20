using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareYunSourse
{
    /// <summary>
    /// 云资源
    /// </summary>
    [Table("YunSourse")]
    public class YunSourse: Entity
    {
        [MaxLength(300)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string URL { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
