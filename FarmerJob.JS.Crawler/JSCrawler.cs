﻿using Abot.Crawler;
using Abot.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FarmerJob.JS.Crawler
{
    public class JSCrawler
    {
        PoliteWebCrawler crawler;
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public JSCrawler()
        {
            crawler = new PoliteWebCrawler();

            crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;


            crawler.ShouldCrawlPage((pageToCrawl, crawlContent) =>
            {
                CrawlDecision decision = new CrawlDecision { Allow = true };

                if (!pageToCrawl.Uri.AbsoluteUri.Contains("/en/job/") && !pageToCrawl.Uri.AbsoluteUri.Contains("/en/job-search/job-vacancy"))
                    return new CrawlDecision { Allow = false, Reason = string.Format("Dont want to crawl {0} pages", pageToCrawl.Uri.AbsoluteUri) };

                return decision;
            });

            crawler.ShouldCrawlPageLinks((crawledPage, crawlContext) =>
            {
                CrawlDecision decision = new CrawlDecision { Allow = true };

                if (!crawledPage.Uri.AbsoluteUri.Contains("/en/job/") && !crawledPage.Uri.AbsoluteUri.Contains("/en/job-search/job-vacancy"))
                    return new CrawlDecision { Allow = false, Reason = "Dont want to crawl links in pages: " + crawledPage.Uri.AbsoluteUri };

                return decision;
            });
        }

        private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            var result = string.Format("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
            log.Info(result);
        }

        private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            var result = string.Format("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
            log.Info(result);
        }

        private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            string result = "";
            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                result = string.Format("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                result = string.Format("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                result = string.Format("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            log.Info("crawler_ProcessPageCrawlCompleted");
            log.Info(result);

            if (!string.IsNullOrEmpty(crawledPage.Content.Text) && crawledPage.Uri.AbsoluteUri.Contains("/en/job/"))
            {
                var doc = crawledPage.HtmlDocument; //Html Agility Pack parser

                //var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

                try
                {
                    string positionTitle = doc.DocumentNode.SelectSingleNode("//h1[@id='position_title']").InnerText.Trim();
                    string location = doc.DocumentNode.SelectSingleNode("//span[@id='single_work_location']").InnerText.Trim();
                    string companyName = doc.DocumentNode.SelectSingleNode("//div[@id='company_name']/a").InnerText.Trim();
                    string postedDate = doc.DocumentNode.SelectSingleNode("//p[@id='posting_date']/span").InnerText.Trim();
                    string jobDescription = doc.DocumentNode.SelectSingleNode("//div[@id='job_description']").InnerHtml.Trim();

                    //log.Info(string.Format("Position Title: {0}\nLocation: {1}\nCompany Name: {2}\nPosted Date: {3}\nJob Desc: {4}", positionTitle, location, companyName, postedDate, jobDescription));
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }

        private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            var result = string.Format("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        public void Crawl(string uri)
        {
            CrawlResult result = crawler.Crawl(new Uri(uri));

            string msg = "";
            if (result.ErrorOccurred)
                msg = string.Format("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
            else
                msg = string.Format("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);

            log.Info(msg);
        }
    }
}
