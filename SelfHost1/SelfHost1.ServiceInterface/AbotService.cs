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
    
    public class AbotService : Service
    {
        
        public async void Any(SiteCrawlModel request)
        {
            new PullentiInf()
                .AnalyzeText(
                await new AbotInf()
                .InitAbot(request.BaseUrl)
                );
        }

    }
}
