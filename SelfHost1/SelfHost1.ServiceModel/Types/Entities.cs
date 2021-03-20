using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel.Types
{
    class Entities
    {
        [PrimaryKey] [AutoIncrement] public int id { get; set; }

        public int pagesid { get; set; }
        
        public string people { get; set; }
        public string organization { get; set; }
        public string country { get; set; }
    }
}
