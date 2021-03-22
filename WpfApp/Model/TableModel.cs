using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using SelfHost1.ServiceModel;

namespace WpfApp.Model
{
    public class TableModel 
    {
        public static async Task<List<Page>> GettAllPagesAsync()
        {
            return (await ClientManager.Client?.SendAsync(new GetPages())).Result;
        }

    }
}
