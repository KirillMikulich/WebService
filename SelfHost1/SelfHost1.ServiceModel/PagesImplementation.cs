using SelfHost1.ServiceModel.Types;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceModel
{
    [Route("/page/delete")]
    public class DeletePage : IReturnVoid
    {
        public int Id { get; set; }
    }
    [Route("/page/create/{url}")]
    public class CreatePage : IReturn<CreatePageResponse>
    {
        public string Url { get; set; }
    }

    public class CreatePageResponse
    {
        public Pages Results { get; set; }
    }
    [Route("/page/get/{id}")]
    public class GetPage : IReturn<GetPageResponse>
    {
        public int Id { get; set; }
    }

    public class GetPageResponse
    {
        public Pages Results { get; set; }
    }
    [Route("/page/update/{id}/{url}")]
    public class UpdateCustomers : IReturn<UpdateCustomersResponse>
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class UpdateCustomersResponse
    {
        public Pages Results { get; set; }
    }
    [Route("/page/getall")]
    public class GetPages : IReturn<GetPagesResponse> { }

    public class GetPagesResponse
    {
        public List<Pages> Results { get; set; }
    }
}
