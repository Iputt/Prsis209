using ps.Common;
using ps.Web.Api.Extension;
using ps.Web.Api.Extension.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ps.Web.Api.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="accountInfo">dto</param>
        /// <returns></returns>
        [HttpPost]
        public string Login([FromBody]LoginFormDto accountInfo)
        {
            ApiObject obj = new ApiObject();
            try
            {
                string apiUrl = AppConfig.GetSetting("default_apiurl", "http://localhost:2018/api/");
                //生成调用数据的接口
                //var api = RestProxy.Create(apiUrl, accountInfo.MANDT, accountInfo.Login, accountInfo.Password, true);
                //string token = api.Token(accountInfo.MANDT, accountInfo.Login, accountInfo.Password, true);
                //result.State = true;
                //result.Info = "登录成功";
                //result.Data = new LoginDto()
                //{
                //    //对登录名和密码进行aes加密，并保存到sessionKey里面 
                //    SessionKey = Extension.Common.AesEncryptor_Base64(string.Join(" ", new string[] { accountInfo.Login, accountInfo.Password })),
                //    Token = token,
                //    Login = accountInfo.Login
                //};
            }
            catch (Exception ex)
            {
                //LogFile.WriteLine($"登录请求出现错误:{ex}");
                //result.State = false;
                //result.Info = ex.Message;
            }
            return "";
        }

        #region ADO .NET 直接操作数据库

        /// <summary>
        /// Ado.Net 获取数据
        /// </summary>
        /// <param name="modelName">对象名</param>
        /// <param name="filterStr">过滤字符串</param>
        /// <param name="keys">查询的key值(必选)</param>
        /// <returns></returns>
        private List<ApiObject> GetData(string modelName, string filterStr, string[] keys)
        {
            //保存查询的数据
            List<ApiObject> results = new List<ApiObject>();
            SqlConnection conn = GetSqlConnection();
            try
            {
                conn.Open();
                //要查询的字段
                string keyStr = keys.Length > 0 ? $"{string.Join(",", keys.ToArray())}" : "*";
                string sqlStr = $"select {keyStr} from {GetModelName(modelName)}";
                //过滤条件
                if (!string.IsNullOrWhiteSpace(filterStr))
                {
                    sqlStr += $" where {filterStr}";
                }
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);
                //返回带key的数据
                using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        ApiObject apiObj = new ApiObject();
                        for (int i = 0; i < keys.Length; i++)
                        {
                            apiObj[keys[i]] = reader[i];
                        }
                        results.Add(apiObj);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteLine($"Ado.Net 查询数据错误:{ex}");
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="modelName">表名</param>
        /// <param name="filterStr">过滤字符串</param>
        /// <returns></returns>
        private int GetCount(string modelName, string filterStr)
        {
            //保存查询的数据
            int count = 0;
            SqlConnection conn = GetSqlConnection();
            try
            {
                conn.Open();
                //要查询的字段 
                string sqlStr = $"select Count(*) from {GetModelName(modelName)}";
                //过滤条件
                if (!string.IsNullOrWhiteSpace(filterStr))
                {
                    sqlStr += $" where {filterStr}";
                }
                SqlCommand sqlCmd = new SqlCommand(sqlStr, conn);
                count =int.Parse(sqlCmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //LogFile.WriteLine($"Ado.Net 查询数据错误:{ex}");
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        /// <summary>
        /// Ado.Net 删除数据
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="filterStr"></param>
        /// <returns></returns>
        private void DeleteData(string modelName, string filterStr)
        {
            //PageResultDto result = new PageResultDto();
            SqlConnection conn = GetSqlConnection();
            StringBuilder sqlsb = new StringBuilder();
            try
            {
                conn.Open();
                sqlsb.Append($"DELETE FROM {GetModelName(modelName)}");
                //过滤字符串不为空,增加过滤条件
                if (!string.IsNullOrWhiteSpace(filterStr))
                {
                    sqlsb.Append($" WHERE {filterStr}");
                }
                using (SqlCommand sqlCmd = new SqlCommand(sqlsb.ToString(), conn))
                {
                    int rowCount = sqlCmd.ExecuteNonQuery();
                    //LogFile.WriteLine($"执行SQL删除操作,SQL: {sqlsb.ToString()}, 受影响行数:{rowCount}");
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteLine($"Ado.Net 删除数据库出现错误,SQL字符串:{sqlsb.ToString()},错误详情:{ex}");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Ado.Net 插入数据
        /// </summary>
        /// <param name="modelName">表名</param>
        /// <param name="objs">数据</param>
        /// <returns></returns>
        private bool InsertData(string modelName, List<ApiObject> objs)
        {
            bool excuteSuccess = true; //是否执行成功
            SqlConnection sqlConnection = GetSqlConnection();
            StringBuilder sqlsb = new StringBuilder();
            try
            {
                sqlConnection.Open();
                foreach (var obj in objs)
                {
                    sqlsb.Append($"INSERT INTO {GetModelName(modelName)} " +
                        $"( {string.Join(",", obj.Select(t => t.Key))}) " +
                        $"VALUES( {string.Join(",", obj.Select(t => "'" + t.Value.ToString() + "'"))} );");
                }
                using (SqlCommand command = new SqlCommand(sqlsb.ToString(), sqlConnection))
                {
                    int effertCount = command.ExecuteNonQuery();
                    //LogFile.WriteLine($"Ado.NET 直接插入数据,SQL语句:{sqlsb.ToString()} ,受影响的行数:{effertCount}");
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteLine($"Ado.NET 直接插入数据错误,SQL语句:{sqlsb.ToString()} , 错误详情:{ex}");
                excuteSuccess = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return excuteSuccess;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="modelName">表名</param>
        /// <param name="filterStr">过滤条件</param>
        /// <param name="changes">变更</param>
        /// <returns></returns>
        //private PageResultDto UpdateData(string modelName, string filterStr, ApiObject changes)
        //{
        //    PageResultDto result = new PageResultDto();
        //    SqlConnection sqlConnection = GetSqlConnection();
        //    StringBuilder sqlsb = new StringBuilder();
        //    try
        //    {
        //        sqlConnection.Open();
        //        sqlsb.Append($"Update {GetModelName(modelName)} SET " +
        //            $"{string.Join(",", changes.Select(t => $"{t.Key}='{t.Value}'"))}");
        //        sqlsb.Append($" WHERE {filterStr} ;");
        //        using (SqlCommand command = new SqlCommand(sqlsb.ToString(), sqlConnection))
        //        {
        //            int count = command.ExecuteNonQuery();
        //            LogFile.WriteLine($"Ado.NET 更新数据,SQL语句:{sqlsb.ToString()} ,受影响的行数:{count}");
        //        }
        //        result.State = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogFile.WriteLine($"Ado.NET 更新数据错误,SQL语句:{sqlsb.ToString()} , 错误详情:{ex}");
        //        result.State = true;
        //        result.Info = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //    return result;
        //}

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetSqlConnection()
        {
            string connStr = AppConfig.GetConnectionString("prsisConStr");
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }

        /// <summary>
        /// 将数据库名的.替换成_
        /// </summary>
        /// <param name="str">数据取值const</param>
        /// <returns></returns>
        private string GetModelName(string str)
        {
            return str.Replace(".", "_");
        }

        #endregion

        /// <summary>
        /// 获取html文件内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private string GetHtml(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    try
                    {
                        byte[] bt = new byte[1024 * 1024 * 2];
                        int r = fs.Read(bt, 0, bt.Length);
                        string htm = Encoding.UTF8.GetString(bt, 0, r);
                        return htm;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 填充模板内容
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="obj">填充参数</param>
        /// <returns></returns>
        private string FormatString(string template, IDictionary<string, object> obj)
        {
            return System.Text.RegularExpressions.Regex.Replace(template, @"{{(\w+)}}", s =>
            {
                var key = s.Groups[1].Value;
                if (obj != null && obj.ContainsKey(key) && obj[key] != null)
                    return obj[key].ToString();
                return "";
            });
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }




        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
