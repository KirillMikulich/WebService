using ServiceStack;
using SelfHost1.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel
{
    public class DeletePage : IReturnVoid
    {
        public int Id { get; set; }
    }

    public class CreatePage : IReturn<CreatePageResponse>
    {
        public string Url { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreatePageResponse
    {
        public Page Result { get; set; }
    }

    public class GetPage : IReturn<GetPageResponse>
    {
        public int Id { get; set; }

    }

    public class GetPageResponse
    {
        public Page Result { get; set; }
    }
    
    public class UpdateCustomers : IReturn<UpdateCustomersResponse>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }

    public class UpdateCustomersResponse
    {
        public Page Results { get; set; }
    }
    public class GetPages : IReturn<GetPagesResponse> { }

    public class GetPagesResponse
    {
        public List<Page> Result { get; set; }
    }
}