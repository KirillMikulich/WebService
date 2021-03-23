using SelfHost1.ServiceModel;
using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using SelfHost1.ServiceInterface;

namespace WpfApp.Model
{
    public class ShowMoreModel
    {
        public static async void UdpatePageById(int id, string title, string text, string html, DateTime date,string url)
        {
            await ClientManager.Client?.SendAsync( new UpdatePageById()
            {
                Id = id,
                Title = title,
                Text = text,
                Html = html,
                Url = url,
                Date = date
            });
        }
    }
}
