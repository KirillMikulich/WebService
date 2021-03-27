
using Pullenti.Morph;
using Pullenti.Ner;
using SelfHost1.ServiceModel.Types;
using Serilog;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost1.ServiceInterface
{
    public class PullentiInf: Service
    {
        public PullentiInf()
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN | MorphLang.BY);
            Pullenti.Ner.Org.OrganizationAnalyzer.Initialize();
            Pullenti.Ner.Person.PersonAnalyzer.Initialize();
            Pullenti.Ner.Geo.GeoAnalyzer.Initialize();
        }

        public void AnalyzeText(List<Page> pages)
        {
            Log.Information("Start analyze text");
            pages.ForEach(pg => {
                var proc = ProcessorService.CreateProcessor();
                var ar = proc.Process(new SourceOfAnalysis(pg.Text));
                
                long Id = Db.Insert(pg, selectIdentity: true);
                Log.Information("Insert record in DataBase");
                Log.Information("Insert record in DataBase Entityes");
                foreach (Referent entity in ar.Entities)
                {
                    Db.Insert(new Entityes
                    {
                        PagesId = Id,
                        Type = entity.TypeName,
                        Entity = entity.ToString().ToUpper()
                    });
                }
            });
            Log.Information("End analyze text");
        }
    }
}
