using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ShareYunSourse.EFCore;
using ShareYunSourse.EFCore.Data;
namespace ShareYunSourse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrationDbContext<YunSourseContext>((context, services) =>
                {
                    new YunSourseContextSeed().SeedAsync(context, services).Wait();
                }).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()//使用Kestrel 服务器
            .UseStartup<Startup>()
            .Build();
    }
}
