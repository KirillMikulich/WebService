using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel
{
    public class SetUrl: IReturnVoid//удалить этот ебаный ужас
    {
        public string Url { get; set; }
    }

}
