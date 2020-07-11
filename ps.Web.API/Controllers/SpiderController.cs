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
    /// <summary>
    /// 爬虫程序
    /// </summary>
    public class SpiderController : Controller
    {
        readonly IAdjustService AdjustService = new AdjustService();

        /// <summary>
        /// 执行抓取数据命令
        /// </summary>
        /// <returns></returns>
        public ActionResult Spider()
        {
            try
            {
                SpiderData("http://muchong.com/bbs/kaoyan.php?action=adjust&type=1");
            }
            catch(Exception e)
            {
                //记录日志
            }
            return View();
        }
        /// <summary>
        /// 抓取数据
        /// </summary>
        /// <param name="url"></param>
        public void SpiderData(string url)
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
            HtmlDocument doc = webClient.Load(url);
            HtmlNodeCollection objects = doc.DocumentNode.SelectNodes("//div[@class='wrapper']/table/tbody[@class='forum_body_manage']/tr");
            int count = 0;
            foreach (var item in objects)
            {
                string basicPath = string.Format("//div[@class='wrapper']/table/tbody[@class='forum_body_manage']/tr[{0}]/", ++count);
                AdjustService.Add(new ps_data_adjust()
                {
                    //后续使用反射优化
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
        }
    }
}