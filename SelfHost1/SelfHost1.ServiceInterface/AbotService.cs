using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using Pullenti.Morph;
using Pullenti.Ner;
using SelfHost1.ServiceModel;
using SelfHost1.ServiceModel.Types;
using Serilog;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    /*
    Pullenti.Ner.Org	NER: организации
    Pullenti.Ner.Person	NER: персона и атрибуты персон
    Pullenti.Ner.Geo	NER: географические объекты
     */
    public class AbotService : Service
    {
        public string BaseUrl { get; set; }
        private List<Page> pages {get;set;}
        List<Entityes> et { get; set; }
        public  void Any(SiteCrawlModel request)
        {
            BaseUrl = request.BaseUrl;
            et = new List<Entityes>();
            
            StartSiteCrawl();
        }

        public void StartSiteCrawl()
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .Enrich.WithThreadId()
               .WriteTo.Console(outputTemplate: Constants.LogFormatTemplate)
               .CreateLogger();

            pages = new List<Page>();
            Log.Information("Start download pages!");
            DemoSimpleCrawler();
            Db.InsertAll(pages);
            Log.Information("End download pages!");
            Log.Information("Start Pullenti Analize");
            PullentiAnalize();
            Log.Information("End Pullenti Analize");
        }

        private  void DemoSimpleCrawler()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 3,
                CrawlTimeoutSeconds = 0,
                IsExternalPageCrawlingEnabled = false,
            };
            var crawler = new PoliteWebCrawler(config);

            crawler.PageCrawlCompleted += Crawler_PageCrawlCompleted;
            Task.Run<CrawlResult>(async()=>await crawler.CrawlAsync(new Uri(BaseUrl))).Wait();

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
        private void DemoSinglePageRequest(string Url)
        {
            var pageRequester = new PageRequester(new CrawlConfiguration(), new WebContentExtractor());

            Task<CrawledPage> task = Task.Run<CrawledPage>(async()=> await  pageRequester.MakeRequestAsync(new Uri(Url)));
            var crawledPage = task.Result;
            if (crawledPage != null)
            {
                string title = crawledPage.AngleSharpHtmlDocument.QuerySelector("h1.name") == null ?
                    crawledPage.AngleSharpHtmlDocument.QuerySelector("h2.name")?.InnerHtml :
                    crawledPage.AngleSharpHtmlDocument.QuerySelector("h1.name")?.InnerHtml;
                var html = crawledPage.Content.Text;
                //div.detail_text .InnerHtml.ToString()
                var text = crawledPage.AngleSharpHtmlDocument.QuerySelectorAll("div.detail_text>div#detailText") ?? 
                    crawledPage.AngleSharpHtmlDocument.QuerySelectorAll("div.detail_text");

                string string_text = null;
                foreach (var x in text)
                {
                    string_text += Regex.Replace(
                        x?.InnerHtml.ToString()
                        , "<[^>]+>", string.Empty);
                }


                string date = crawledPage.AngleSharpHtmlDocument.QuerySelector("span.news-date-time")?.ToString();

                if (title != null && html != null && string_text != null && date != null)
                {
                    pages.Add(new Page
                    {
                        Title = title,
                        Url = Url,
                        Html = html,
                        Date = DateTime.Now, // DateTime.Parse(date),
                        Text = string_text
                    });
                    
                }
            }
        }

        private async void PullentiAnalize()
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN | MorphLang.BY);
            Pullenti.Ner.Org.OrganizationAnalyzer.Initialize();
            Pullenti.Ner.Person.PersonAnalyzer.Initialize();
            Pullenti.Ner.Geo.GeoAnalyzer.Initialize();
            List<Page> pg = Db.Select<Page>();
            foreach (Page p in pg)
            {

                Log.Information("Page id = "+p.Id+" analize");
                var proc = ProcessorService.CreateProcessor();
                var ar = proc.Process(new SourceOfAnalysis(p.Text));


                foreach (Referent entity in ar.Entities)
                {
                    et.Add(new Entityes
                    {
                        PagesId = p.Id,
                        Type = entity.TypeName,
                        Entity = entity.ToString().ToUpper()
                    });
                }
            }
            Db.InsertAll(et);
        }
    }
}
