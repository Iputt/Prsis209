using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PS.API.Controllers
{
    /// <summary>
    /// 通用接口
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public CommonController(ILogger<CommonController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Ps API 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Demo()
        {
            return Ok();
        }
    }
}