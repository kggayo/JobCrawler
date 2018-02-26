using FamerJob.BJ.Crawler;
using HtmlAgilityPack;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmerJob.BJ.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            BJCrawler crawler = new BJCrawler();
            crawler.Crawl(@"https://www.bestjobs.ph/jobs-of-computers-telecommunication-in-cebu?prov=7&by=publicationtime");

            //HtmlDocument doc = new HtmlDocument();
            //doc.Load(@"html\index.html");

            //string positionTitle = doc.DocumentNode.SelectSingleNode("//section[@class='box box_r']/ul/li/p").InnerText.Trim();
            //string location = doc.DocumentNode.SelectNodes("//div[@class='cm-12 box_i bWord']/ul/li")[1].InnerText.Trim();
            //string companyName = doc.DocumentNode.SelectSingleNode("//a[@id='urlverofertas']").InnerText.Trim();
            //string postedDate = doc.DocumentNode.SelectSingleNode("//div[@class='cm-12 box_i bWord']/ul/p/span[@class='info_pub']/span").InnerText.Trim();
            //string jobDescription = doc.DocumentNode.SelectNodes("//div[@class='cm-12 box_i bWord']/ul/li")[2].InnerHtml.Trim();

            //System.Console.WriteLine("Position Title: {0}\nLocation: {1}\nCompany Name: {2}\nPosted Date: {3}\nJob Desc: {4}", positionTitle, location, companyName, postedDate, jobDescription);

            System.Console.WriteLine("Done");
            System.Console.ReadLine();
        }
    }
}
