using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel.Types
{
    public class Pages
    {
        [PrimaryKey] [AutoIncrement] public int id { get; set; }
        public string url { get; set; }
    }
}
