using NUnit.Framework;
using SelfHost1.ServiceInterface;
using SelfHost1.ServiceModel;
using ServiceStack;
using ServiceStack.Testing;

namespace SelfHost1.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<PagesService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
           
        }
    }
}
