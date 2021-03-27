using SelfHost1.ServiceModel.Types;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel
{
    public class SearchEntity : IReturn<SearchEntityResponse>
    {
        public string Word { get; set; }
    }

    public class SearchEntityResponse
    {
        public List<Page> Result { get; set; }
    }

}
