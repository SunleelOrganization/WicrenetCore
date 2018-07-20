using ShareYunSourse.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareYunSourse.Application.Dto
{
    public class GetUserInput : PagedAndSortedInputDto
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
