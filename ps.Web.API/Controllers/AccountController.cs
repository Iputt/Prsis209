using ps.Web.Api.Application;
using ps.Web.Api.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ps.Web.Api.Controllers
{
    /// <summary>
    /// 获取数据接口
    /// </summary>
    public class AccountController : ApiController
    {
        /// <summary>
        /// 登录接口（测试用）
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginResultDto Login([FromBody]LoginFormDto accountInfo)
        {
            LoginResultDto result = new LoginResultDto();
            try
            {
                result.State = true;
                result.Info = "登录成功";
                result.Data = new VerifyDto()
                {
                    //对登录名和密码进行aes加密，并保存到sessionKey里面 
                    SessionKey = Extension.Common.AesEncryptor_Base64(string.Join(" ", new string[] { accountInfo.username, accountInfo.pwd })),
                    Token = "",
                    Login = accountInfo.username
                };
            }
            catch (Exception e)
            {
                //LogFile.WriteLine($"登录请求出现错误:{e}");
                result.State = false;
                result.Info = e.Message;
            }
            return result;
        }

    }
}