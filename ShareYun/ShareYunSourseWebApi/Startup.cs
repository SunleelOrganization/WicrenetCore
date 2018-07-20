using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Azure.KeyVault.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using ShareYunSourse.Application;
using ShareYunSourse.Application.ShareYunSourse;

namespace ShareYunSourseWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           // services.AddScoped(typeof(IShareYunSourseAppService),typeof(ShareYunSourseAppService));
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1", //版本号
                    Title = "CORE接口文档", //标题
                    Description = "RESTful API ",
                    TermsOfService = "",//服务的条件
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "sunleel", Email = "206978828@qq.com", Url = "18217262770" }
                });
                //获取设置配置信息的 的路径对象   swagger界面配置
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "AspNetCoreApiSwagger.xml");
                x.IncludeXmlComments(xmlPath);
                x.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            // 指定站点
            app.UseSwaggerUI(x =>
            {
                //做出一个限制信息 描述
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "TwBusManagement API V1");
                //显示在发出请求时发送的标题
                x.ShowRequestHeaders();
            });
        }
    }
}
