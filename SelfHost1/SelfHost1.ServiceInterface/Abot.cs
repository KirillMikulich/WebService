using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using SelfHost1.ServiceModel.Types;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    public class AbotInf
    {
        private string BaseUrl { get; set; }
        private List<Page> pages { get; set; }
    
        public async Task<List<Page>> InitAbot(string url)
        {
            BaseUrl = url;
            pages = new List<Page>();
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: Constants.LogFormatTemplate)
               .CreateLogger();
            Log.Information("Start download pages");
            await DemoSimpleCrawler();
            Log.Information("End download pages");
            return pages;
        }

        private async Task DemoSimpleCrawler()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 3,
                CrawlTimeoutSeconds = 0,
                IsExternalPageCrawlingEnabled = false,
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted;
            await crawler.CrawlAsync(new Uri(BaseUrl));
        }
        private void Crawler_PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            var htmlAgilityPackDocument = crawledPage.AngleSharpHtmlDocument;
            var c = htmlAgilityPackDocument.QuerySelectorAll("div.preview_text a");
            
            foreach (var x in c)
            {
                DemoSinglePageRequest(BaseUrl + x.GetAttribute("href").Remove(0, 1));

            }
        }
        private async void DemoSinglePageRequest(string Url)
        {
            var pageRequester = new PageRequester(new CrawlConfiguration(), new WebContentExtractor());

            var crawledPage = await pageRequester.MakeRequestAsync(new Uri(Url));
            if (crawledPage != null)
            {
                string title = crawledPage.AngleSharpHtmlDocument.QuerySelector("h1.name") == null ?
                    crawledPage.AngleSharpHtmlDocument.QuerySelector("h2.name")?.InnerHtml :
                    crawledPage.AngleSharpHtmlDocument.QuerySelector("h1.name")?.InnerHtml;
                var html = crawledPage.Content.Text;
                var text = crawledPage.AngleSharpHtmlDocument.QuerySelectorAll("div.detail_text>div#detailText") ??
                    crawledPage.AngleSharpHtmlDocument.QuerySelectorAll("div.detail_text");

                string string_text = null;
                foreach (var x in text)
                {
                    string_text += Regex.Replace(
                        x?.InnerHtml.ToString()
                        , "<[^>]+>", string.Empty);
                }


                string date = crawledPage.AngleSharpHtmlDocument.QuerySelector("span.news-date-time.news_date")?.InnerHtml.ToString().Remove(0, 3);
                
                if (title != null && html != null && string_text != null && date != null)
                {
                    pages.Add(new Page
                    {
                        Title = title,
                        Url = Url,
                        Html = html,
                        Date = DateTime.Parse(date).Date,
                        Text = string_text
                    });
                }
            }
        }
    }
}
