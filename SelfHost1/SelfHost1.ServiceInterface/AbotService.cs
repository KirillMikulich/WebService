using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using SelfHost1.ServiceModel;
using Serilog;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    public class AbotService: Service
    {
        private static string BaseUrl = "https://belaruspartisan.by/";

        public async void Any(SetUrl request)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: Constants.LogFormatTemplate)
               .CreateLogger();

            Log.Information("Start download pages.");

            await DemoSimpleCrawler();

            Log.Information("End download pages!");
        }
        private  async Task DemoSimpleCrawler()
        {
            var config = new CrawlConfiguration
            {
                CrawlTimeoutSeconds = 0,
                IsExternalPageCrawlingEnabled = false,
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted;
             
            await crawler.CrawlAsync(new Uri(BaseUrl));

        }
        private async void Crawler_PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            var htmlAgilityPackDocument = crawledPage.AngleSharpHtmlDocument; 
            var c = htmlAgilityPackDocument.QuerySelectorAll("div.preview_text a");
            foreach(var x in c)
            {
                 await  DemoSinglePageRequest(BaseUrl+x.GetAttribute("href").Remove(0, 1));
            }
        
        }

        private  async Task DemoSinglePageRequest(string Url)
        {
            var pageRequester = new PageRequester(new CrawlConfiguration(), new WebContentExtractor());

            var crawledPage = await pageRequester.MakeRequestAsync(new Uri(Url));

            string title = crawledPage.AngleSharpHtmlDocument
                .QuerySelector("h1.name")
                .InnerHtml;
            var html = crawledPage.Content.Text;
            var text = Regex.Replace(crawledPage.AngleSharpHtmlDocument
                .QuerySelector("div.detail_text>div#detailText")
                .InnerHtml
                .ToString(), 
                "<[^>]+>", string.Empty);
            string date = crawledPage.AngleSharpHtmlDocument.QuerySelector("span.news-date-time").ToString();

            new DbPageService().Any(new CreatePage
            {
                Title = title,
                Url = Url,
                Html = html,
                Date = DateTime.Now,//допилить дату 
                Text = text
            });
            var x = 0;
        }
    }
}
