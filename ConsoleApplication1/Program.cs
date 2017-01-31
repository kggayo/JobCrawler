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
            crawler.Crawl(@"https://cebu.mynimo.com/jobs");

            System.Console.ReadLine();
        }
    }
}
