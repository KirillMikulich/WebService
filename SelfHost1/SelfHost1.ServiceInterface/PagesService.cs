using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfHost1.ServiceModel.Types;
using ServiceStack.OrmLite;
using SelfHost1.ServiceModel;

namespace SelfHost1.ServiceInterface
{
    public class PagesService: Service
    {
        public GetPagesResponse Any(GetPages request)
        {
            return new GetPagesResponse { Results = Db.Select<Pages>() };
        }

        public GetPageResponse Any(GetPage request)
        {
            var page = Db.SingleById<Pages>(request.id);
            if (page == null)
                throw HttpError.NotFound("Page not found");

            return new GetPageResponse
            {
                Results = Db.SingleById<Pages>(request.id)
            };
        }

        public CreatePageResponse Any(CreatePage request)
        {
            var page = new Pages { url = request.url};
            Db.Save(page);
            return new CreatePageResponse
            {
                Results = page
            };
        }

        public UpdateCustomersResponse Any(UpdateCustomers request)
        {
            var page = Db.SingleById<Pages>(request.id);
            if (page == null)
                throw HttpError.NotFound("Page '{0}' does not exist".Fmt(request.id));

            page.url = request.url;
            Db.Update(page);

            return new UpdateCustomersResponse
            {
                Results = page
            };
        }

        public void Any(DeletePage request)
        {
            Db.DeleteById<Pages>(request.id);
        }
    }
}
