using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmerJob.Crawler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            JobCrawler crawler = new JobCrawler();
            crawler.Crawl(@"https://cebu.mynimo.com/jobs/browse/it");
            //crawler.Crawl(@"https://cebu.mynimo.com/jobs/browse/programming");

            //HtmlDocument doc = new HtmlDocument();
            //doc.Load(@"html\index.html");

            //string sourceUrl = "https://cebu.mynimo.com/jobs/browse/it";
            //string jobUrl = "test.html";

            //string positionTitle = doc.DocumentNode.SelectSingleNode("//h1[@class='search_highlight']").InnerText.Trim();
            //string location = doc.DocumentNode.SelectSingleNode("//div[@class='location search_highlight']").InnerText.Trim();
            //string companyName = doc.DocumentNode.SelectSingleNode("//span[@class='search_highlight']//a").InnerText.Trim();
            //string postedDate = doc.DocumentNode.SelectSingleNode("//div[@id='contentHeading']//div[@class='meta']").InnerText.Trim().Split(new char[] { '\n' })[0].Replace("Posted on : ","");
            //string jobDescription = doc.DocumentNode.SelectSingleNode("//meta[@name='description']").Attributes["content"].Value;

            //System.Console.WriteLine("Position: " + posistionTitle + "\nCompany:" + companyName);
            System.Console.WriteLine("Done");
            System.Console.ReadLine();
        }

        private static void PostJob(string sourceUrl, string jobUrl, string positionTitle, string location, string companyName, string jobPostedDate, string jobDescription, string yrsOfExperience)
        {
            
        }
    }
}
