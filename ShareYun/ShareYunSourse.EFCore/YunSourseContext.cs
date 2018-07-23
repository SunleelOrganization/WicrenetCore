using Microsoft.EntityFrameworkCore;
using ShareYunSourse.Core;
using System;

namespace ShareYunSourse.EFCore
{
    public class YunSourseContext : DbContext
    {
        public YunSourseContext(DbContextOptions<YunSourseContext> option)
            : base(option)
        {
        }

        public DbSet<YunSourse> YunSourse { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }


    }
}
