using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel.Types
{
    class TextPages
    {
        [PrimaryKey] [AutoIncrement] public int id { get; set; }

        public int pageid { get; set; }
        public string html { get; set; }
        public string title { get; set; }
        public string textnothtml { get; set; }
        public DateTime date { get; set; }
    }
}
