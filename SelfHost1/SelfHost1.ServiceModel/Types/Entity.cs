using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel.Types
{
    public class Entityes
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        [References(typeof(Page))]
        public long PagesId { get; set; }
        public string Type { get; set; }
        public string Entity { get; set; }
    }
}
