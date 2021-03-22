using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class ClientManager
    {
        public static IServiceClient Client { get; set; } = new CachedHttpClient(new JsonHttpClient("http://localhost:8088"));
        //private var serviceUrl = ConfigurationManager.AppSettings.Get("http://localhost:8088");
        //var client = new JsonServiceClient(serviceUrl)

    }
}
