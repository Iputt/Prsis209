using HtmlAgilityPack;
using ps.module.BLL;
using ps.module.IBLL;
using ps.module.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ps.Web.Api.Controllers
{
    public class SpiderController : Controller
    {
        readonly IAdjustService AdjustService = new AdjustService();


        // GET: Spider
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Spider()
        {
            HtmlWeb webClient = new HtmlWeb();
            webClient.OverrideEncoding = Encoding.GetEncoding("gb2312");
            HtmlAgilityPack.HtmlWeb.PreRequestHandler handler = delegate (HttpWebRequest request)
            {
                request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };
            webClient.PreRequest += handler;
            HtmlDocument doc = webClient.Load("http://muchong.com/bbs/kaoyan.php?action=adjust&type=1");
            HtmlNodeCollection objects = doc.DocumentNode.SelectNodes("//div[@class='wrapper']/table/tbody[@class='forum_body_manage']/tr");
            int count = 0;
            foreach (var item in objects)
            {
                string basicPath = string.Format( "//div[@class='wrapper']/table/tbody[@class='forum_body_manage']/tr[{0}]/",++count);
                AdjustService.Add(new ps_data_adjust()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    p_title = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText,
                    p_college = doc.DocumentNode.SelectSingleNode(basicPath + "td[2]").InnerText,
                    p_major = doc.DocumentNode.SelectSingleNode(basicPath + "td[3]").InnerText,
                    p_learnStyle = "",
                    p_enrolment = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(basicPath + "td[4]").InnerText),
                    p_releaseTime = DateTime.Now,
                    p_contactMode = DateTime.Now,
                    p_content = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText,
                    p_spare = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText,
                    IsDeleted = "0"
                });
            }
            return View();
            //for (int j = 1; j < objects.Count; j++)
            //{
            //    //string url = doc.DocumentNode.SelectSingleNode("//div[@class='houseList']/dl[@dataflag='bg']["+j+"]/dt/a").Attributes["href"].Value;
            //    //string imgurl = doc.DocumentNode.SelectSingleNode("//div[@class='houseList']/dl[@dataflag='bg'][" + j + "]/dt/a/img").Attributes["src"].Value;
            //    string basicPath = "//div[@class='wrapper']/table/tbody[@class='forum_body_manage']/tr[" + j + "]/";
            //    string title = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText;
            //    string college = doc.DocumentNode.SelectSingleNode(basicPath + "td[2]").InnerText;

            //    string major = doc.DocumentNode.SelectSingleNode(basicPath + "td[3]").InnerText;

            //    string learntype = "";
            //    string countstr = doc.DocumentNode.SelectSingleNode(basicPath + "td[4]").InnerText;
            //    int count = int.Parse(countstr);
            //    string contract = "";
            //    string content = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText;
            //    string spare = doc.DocumentNode.SelectSingleNode(basicPath + "td[@class='xmc_lp20']/a[@class='xmc_ft12']").InnerText;
            //    DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            //    char num = '0';
            //    Guid guid = Guid.NewGuid();
            //    SqlParameter[] sps ={
            //    new SqlParameter("@Id",guid),
            //    new SqlParameter("@Created",now),
            //    new SqlParameter("@p_title",title),
            //    new SqlParameter("@p_college",college),
            //    new SqlParameter("@p_major",major),
            //    new SqlParameter("@p_learnStyle",learntype),
            //    new SqlParameter("@p_enrolment",count),
            //    new SqlParameter("@p_releaseTime",now),
            //    new SqlParameter("@p_contactMode",contract),
            //    new SqlParameter("@p_content",content),
            //    new SqlParameter("@p_spare",spare),
            //    new SqlParameter("@IsDeleted",num),
            //    };
            //    string sql = "INSERT INTO ps_data_adjust values(@Id,@Created,@p_title,@p_college,@p_major,@p_learnStyle,@p_enrolment,@p_releaseTime,@p_contactMode,@p_content,@p_spare,@IsDeleted)";
            //    try
            //    {
            //        //DbHelperSQL.ExecuteSql(sql, ConfigHelper.GetConnectionString("conn"), sps);
            //    }
            //    //catch (Exception ex)
            //    catch
            //    {
            //        //Response.Write(ex.Message.ToString());
            //        throw;
            //    }
            //}
        }
    }
}