using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
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
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    public class AbotService: Service
    {
        public async void Any(SiteCrawl request)
        {
           await request.StartSiteCrawl();
            //уточнить как сделать

            foreach(Page page in request.pg)
            {
                Log.Information("Insert");
                Db.Save(page);
            }
        }
        
    }
}
