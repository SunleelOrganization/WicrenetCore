using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareYunSourse.Application;

namespace ShareYunSourseWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
       // public IShareYunSourseAppService _shareYunSourseAppService;
        public ValuesController()//IShareYunSourseAppService shareYunSourseAppService)
        {
         //_shareYunSourseAppService = shareYunSourseAppService;
        }
        /// <summary>
        /// GET 请求
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///访问参数
        ///     POST 
        ///     {  
        ///        "value": "1",  
        ///     }  
        ///   
        /// </remarks> 
        /// <response code="1">返回1</response>
        /// <response code="2">返回2</response> 
        [HttpGet]
        public int Get([FromBody]int value)
        {
            return 1;
        }    
    }
}
