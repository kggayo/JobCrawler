using FarmerJob.JS.Crawler;
using HtmlAgilityPack;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmerJob.JS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlConfigurator.Configure();
            //JSCrawler crawler = new JSCrawler();
            //crawler.Crawl(@"https://www.jobstreet.com.ph/en/job-search/job-vacancy.php?key=&location=60800&specialization=191%2C192%2C193&salary=&area=1&ojs=5&src=12&sort=&order=");

            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"html\index.html");

            string positionTitle = doc.DocumentNode.SelectSingleNode("//h1[@id='position_title']").InnerText.Trim();
            string location = doc.DocumentNode.SelectSingleNode("//span[@id='single_work_location']").InnerText.Trim();
            string companyName = doc.DocumentNode.SelectSingleNode("//div[@id='company_name']/a").InnerText.Trim();
            string postedDate = doc.DocumentNode.SelectSingleNode("//p[@id='posting_date']/span").InnerText.Trim();
            string jobDescription = doc.DocumentNode.SelectSingleNode("//div[@id='job_description']").InnerHtml.Trim();

            System.Console.WriteLine("Position Title: {0}\nLocation: {1}\nCompany Name: {2}\nPosted Date: {3}\nJob Desc: {4}", positionTitle, location, companyName, postedDate, jobDescription);

            System.Console.WriteLine("Done");
            System.Console.ReadLine();
        }
    }
}
