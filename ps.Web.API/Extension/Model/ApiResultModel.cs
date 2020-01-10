using System.Net;

namespace ps.Web.Api.Extension.Model
{
    public class ApiResultModel
    {
        private HttpStatusCode statusCode;
        private object data;
        private string errorMessage;
        private bool isSuccess;

        /// <summary>
        /// 状态代码
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        } 
    }
}