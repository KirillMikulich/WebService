using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using SelfHost1.ServiceModel.Types;
using Serilog;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel
{
    public class SiteCrawlModel : IReturnVoid
    {
        public  string BaseUrl  { get; set; }
        
    }
}
