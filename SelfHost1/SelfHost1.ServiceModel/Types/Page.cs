using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel.Types
{
    public class Page
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        public string Url { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string Text{ get; set; }
        public DateTime Date { get; set; }
    }
}
