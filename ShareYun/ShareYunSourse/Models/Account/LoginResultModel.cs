using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareYunSourse.Web.Models.Account
{
    public class LoginResultModel
    {
        public string Error { get; set; }
        public string SuccInfo { get; set; }
        public string ReturnUrl { get; set; }

    }
}
