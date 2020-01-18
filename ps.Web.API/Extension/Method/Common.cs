using Newtonsoft.Json.Linq;
using ps.Web.Api.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ps.Web.Api.Extension
{/// <summary>
/// API要用得通用方法写在这里
/// </summary>
    public class Common
    {
        /// <summary>
        /// 获取API Client
        /// </summary>
        /// <returns></returns>
        public static ApiClient GetApiClient()
        {
            //sessionKey
            string sessionKey = HttpContext.Current.Request.Headers["sessionKey"].ToString();
            try
            {
                /*
                 * AES解密 
                 * 支持token登录之后需将这一段代码删除
                 */
                string loginAndPwd = Common.AesDecryptor_Base64(sessionKey);
                var arr = loginAndPwd.Split(' ');
                string apiUrl = "http://localhost:2018/api/";
                //请求2018接口 实现登录功能
                ApiClient apiClient = new ApiClient(apiUrl, arr[0], arr[1]);
                apiClient.Login();


                //var apiClient = HttpContext.Current.Session[sessionKey] as ApiClient;
                ////token为空 证明 session 不存在 也要抛出异常
                //if (string.IsNullOrWhiteSpace(apiClient.Token))
                //{
                //    throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
                //}
                return apiClient;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// 获取当前登录用户 Token登录做好之后要改
        /// </summary>
        /// <returns></returns>
        public static string GetLogin()
        {
            //sessionKey
            string sessionKey = HttpContext.Current.Request.Headers["sessionKey"].ToString();
            return Common.AesDecryptor_Base64(sessionKey).Split(' ')[0];
        }

        /// <summary>
        /// 获取查询数据的APIClient
        /// </summary>
        /// <returns></returns>
        //public static IRestfulApi GetRestfulApi()
        //{
        //    //sessionKey
        //    string sessionKey = HttpContext.Current.Request.Headers["sessionKey"].ToString2();
        //    try
        //    {
        //        /*
        //         * AES解密 
        //         * 支持token登录之后需将这一段代码删除
        //         */
        //        string loginAndPwd = Common.AesDecryptor_Base64(sessionKey);
        //        var arr = loginAndPwd.Split(' ');
        //        string mandt = AppConfig.GetSetting("default_mandt", "");
        //        string apiUrl = AppConfig.GetSetting("default_apiurl", "http://localhost:2018/");
        //        //生成调用数据的接口
        //        var api = RestProxy.Create(apiUrl, mandt, arr[0], arr[1], true);

        //        //var apiClient = HttpContext.Current.Session[sessionKey] as ApiClient;
        //        ////token为空 证明 session 不存在 也要抛出异常
        //        //if (string.IsNullOrWhiteSpace(apiClient.Token))
        //        //{
        //        //    throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
        //        //}
        //        return api;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
        //    }

        //}

        /// <summary>
        /// AES 算法加密(ECB模式) 将明文加密，加密后进行base64编码，返回密文
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>加密后base64编码的密文</returns>
        public static string AesEncryptor_Base64(string encryptStr,
            string key = "O4ysGGvWeHJzw/nfNRuFaFF/aTK4iBqJcxzx/O8wsp4=",
            string iv = "t3JK+LlpfFdhVTpKGN6K1Q==")
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(key);
                byte[] ivArray = Convert.FromBase64String(iv);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.IV = ivArray;
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// AES 算法解密(ECB模式) 将密文base64解码进行解密，返回明文
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>明文</returns>
        public static string AesDecryptor_Base64(string decryptStr,
            string key = "O4ysGGvWeHJzw/nfNRuFaFF/aTK4iBqJcxzx/O8wsp4=",
            string iv = "t3JK+LlpfFdhVTpKGN6K1Q==")
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(key);
                byte[] ivArray = Convert.FromBase64String(iv);
                byte[] toEncryptArray = Convert.FromBase64String(decryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}