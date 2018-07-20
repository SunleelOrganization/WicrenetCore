using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using ShareYunSourse.EFCore;
using Microsoft.EntityFrameworkCore;
using ShareYunSourse.Application;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Linq;
using ShareYunSourse.Core.Dependency;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using System.IO;
using ShareYunSourse.EFCore.UOW;

namespace ShareYunSourse
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("KestrelServerOptions.json")//服务器配置JSON文件
           .AddJsonFile("appsettings.json").Build();
        }
        public IContainer ApplicationContainer { get; private set; }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.Name = "Asp.NetCore";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                      {
                    o.LoginPath = new PathString("/Account/Login");
                       });
            services.Configure<KestrelServerOptions>(Configuration);//配置Kestrel服务器

            services.AddDbContext<YunSourseContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default"), b => b.UseRowNumberForPaging()));//配置sqlserver


            services.AddScoped<IUnitOfWork,UnitOfWork<YunSourseContext>>();//注入UOW依赖，确保每次请求都是同一个对象

            var assemblys = Assembly.Load("ShareYunSourse.Application");//获取ShareYunSourse.Application 程序集

            var builder = new ContainerBuilder();//实例化 AutoFac   
            builder.RegisterGeneric(typeof(EfRepositoryBase<>)).As(typeof(IRepository<>)).InstancePerDependency();//注册仓储泛型
            var baseType = typeof(IDependency);
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.RegisterAssemblyTypes(assemblys)
                .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            //services.AddMiniProfiler(options =>//使用MiniProfiler，前端 监控 SQL执行情况
            //{
            //    options.RouteBasePath = "/profiler";

            //}).AddEntityFramework();
          
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseSession();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"ViewJS")
                    ),
                RequestPath = new PathString("/StaticFile"),
                OnPrepareResponse = ctx => { ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600"); }
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
