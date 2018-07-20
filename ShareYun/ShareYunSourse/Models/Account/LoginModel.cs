using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShareYunSourse.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPwd { get; set; }
        public int? IsDeprt { get; set; }

        public string VerCode { get; set; }

    }
}
