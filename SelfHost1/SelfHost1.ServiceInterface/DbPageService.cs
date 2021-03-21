using SelfHost1.ServiceModel;
using SelfHost1.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    public class DbPageService: Service
    {
        public GetPagesResponse Any()
        {
            return new GetPagesResponse { Result = Db.Select<Page>() };
        }

        public GetPageResponse Any(GetPage request)
        {
            var page = Db.SingleById<Page>(request.Id);
            if (page == null)
                throw HttpError.NotFound("Page not found");

            return new GetPageResponse
            {
                Result = Db.SingleById<Page>(request.Id)
            };
        }

        public CreatePageResponse Any(CreatePage request)
        {
            var page = new Page {   Url = request.Url,
                                    Html = request.Html,
                                    Title = request.Title,
                                    Text = request.Text,
                                    Date = request.Date
            };
            Db.Save(page);
            return new CreatePageResponse
            {
                Result = page
            };
        }

        public UpdateCustomersResponse Any(UpdateCustomers request)
        {
            var page = Db.SingleById<Page>(request.Id);
            if (page == null)
                throw HttpError.NotFound("Page '{0}' does not exist".Fmt(request.Id));

            page.Url = request.Url;
            page.Html = request.Html;
            page.Title = request.Title;
            page.Text = request.Text;
            page.Date = request.Date;

            Db.Update(page);

            return new UpdateCustomersResponse
            {
                Results = page
            };
        }

        public void Any(DeletePage request)
        {
            Db.DeleteById<Page>(request.Id);
        }

    }
}
