using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ShareYunSourse.EFCore.Data
{
    public class YunSourseContextSeed
    {

        public async Task SeedAsync(YunSourseContext context,IServiceProvider service)
        {
            if (!context.User.Any())
            {
                var defaultUser = new User { UserName = "sunleel", UserPwd = "sunleel", Email = "838283226@qq.com", Age = 27, CreationTime = DateTime.Now };
                await context.User.AddAsync(defaultUser);
                await context.SaveChangesAsync();
            }

        }
    }
}
