using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ps.Web.Api.Application
{
    public class Response
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 结果类型 字符串或值类型为 simple 其他为类型名称小写
        /// </summary>
        public string datatype { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public object data { get; set; }

    }
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 不排序
        /// </summary>
        Unspecified = 0,
        /// <summary>
        /// 顺序排序
        /// </summary>
        Ascending = 1,
        /// <summary>
        /// 逆序排序
        /// </summary>
        Descending = 2
    }

    public class ApiObject : Dictionary<string, object>
    {
        public ApiObject()
        {

        }

        public ApiObject(ApiObject obj)
        {
            foreach (var pair in obj)
            {
                this[pair.Key] = pair.Value;
            }
        }

        protected T GetValue<T>(string key)
        {
            if (this.ContainsKey(key))
                return (T)this[key];
            return default(T);
        }
        protected void SetValue(string key, object value)
        {
            this[key] = value;
        }
        /// <summary>
        /// 获取调用当前方法的字段名称
        /// </summary>
        /// <returns></returns>
        protected string GetPropertyName()
        {
            var fname = new StackFrame(2).GetMethod().Name;
            return fname.Substring(4);
        }
        /// <summary>
        /// 获取当前属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetValue<T>()
        {
            return GetValue<T>(GetPropertyName());
        }

        /// <summary>
        /// 设置当前属性的值
        /// </summary>
        /// <param name="value"></param>
        protected void SetValue(object value)
        {
            SetValue(GetPropertyName(), value);
        }

        [JsonIgnore]
        public string __ModelName__
        {
            get
            {
                return GetValue<string>();// this.ContainsKey("__ModelName__") ? this["__ModelName__"] as string : "";
            }
            set { SetValue(value); }
        }
        [JsonIgnore]
        public Guid Id
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }
    }

    public class ApiClient
    {
        public string Url { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Token { get; private set; }



        public ApiClient(string url, string username, string pwd)
        {

            Url = url;
            if (!Url.EndsWith("/"))
                Url = url + "/";
            UserName = username;
            Password = pwd;
        }
        static string SerialJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        static T DeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        protected object fromToken(JToken token)
        {
            if (token.Type == JTokenType.Array)
            {
                List<object> objs = new List<object>();
                foreach (var v in token)
                {
                    objs.Add(fromToken(v));
                }
                return objs;
            }
            else if (token.Type == JTokenType.Object)
            {
                ApiObject obj = new ApiObject();
                foreach (JProperty property in token)
                {
                    obj[property.Name] = fromToken(property.Value);
                }
                return obj;
            }
            else
            {
                switch (token.Type)
                {
                    case JTokenType.Null:
                    case JTokenType.None:
                    case JTokenType.Undefined: return null;
                    default: return ((JValue)token).Value;
                }
            }
        }
        object DoRequest(string action, Dictionary<string, object> param)
        {
            if (action != "Token" && string.IsNullOrEmpty(Token))
                Login();

            var url = string.Format("{0}{1}", this.Url, action);

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";

            var str = string.Format("req=remote&data={0}", HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(SerialJson(param))));

            var data = Encoding.UTF8.GetBytes(str);
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            if (action != "Token")
                request.Headers["token"] = Token;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var restext = "";
            using (var response = request.GetResponse())
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    restext = reader.ReadToEnd();
                }
            }
            var result = DeJson<Response>(restext);
            if (result.type == "error")
                throw new Exception(result.message);
            if (result.data is JToken)
                return fromToken(result.data as JToken);
            return result.data;
        }
        /// <summary>
        /// 执行登录
        /// </summary>
        public void Login()
        {
            var param = new Dictionary<string, object>();
            param["username"] = UserName;
            param["password"] = Password;
            param["persistent"] = false;
            Token = DoRequest("Token", param) as string;
        }

        public List<ApiObject> FindById(string modelname, Guid[] id, params string[] selector)
        {
            var param = new Dictionary<string, object>();
            param["modelname"] = modelname;
            param["id"] = id;
            param["selector"] = selector;
            return (DoRequest("read", param) as List<object>).ConvertAll<ApiObject>(s => s as ApiObject);
        }
        public ApiObject FindById(string modelname, Guid id, params string[] selector)
        {
            var reuslt = FindById(modelname, new Guid[] { id }, selector);
            return reuslt.Count == 0 ? null : reuslt[0] as ApiObject;
        }

        public List<ApiObject> GetList(string modelname, string specification, Dictionary<string, SortOrder> sortby, int start = 0, int size = 0, params string[] selector)
        {
            var param = new Dictionary<string, object>();
            param["modelname"] = modelname;
            param["specification"] = specification;
            param["sortby"] = sortby;
            param["start"] = start;
            param["selector"] = selector;
            return (DoRequest("getlist", param) as List<object>).ConvertAll<ApiObject>(s => s as ApiObject);
        }

        public int Count(string modelname, string specification)
        {
            var param = new Dictionary<string, object>();
            param["modelname"] = modelname;
            param["specification"] = specification;
            return Convert.ToInt32(DoRequest("count", param));
        }

        public IDictionary<string, object> Sum(string modelname, string specification, params string[] selector)
        {
            var param = new Dictionary<string, object>();
            param["modelname"] = modelname;
            param["specification"] = specification;
            param["selector"] = selector;
            return DoRequest("sum", param) as Dictionary<string, object>;
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="roots">添加/修改</param>
        /// <param name="keys">删除</param>
        public void Save(IEnumerable<ApiObject> adds, IEnumerable<ApiObject> updates, IEnumerable<ApiObject> removes)
        {
            var param = new Dictionary<string, object>();
            param["adds"] = adds;
            param["updates"] = updates;
            param["removes"] = removes;
            DoRequest("save", param);
        }
    }
}
