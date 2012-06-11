using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using StructureMap.Configuration.DSL;

namespace ForeverRobot.Robots.Infrastructure
{
    public class RavenDbBootstrapper : Registry
    {
        private const string TenantName = "Robot";
        public RavenDbBootstrapper()
        {

            For<IDocumentStore>().Singleton().Use(CurrentDocumentStore());
            For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(x => x.GetInstance<IDocumentStore>().OpenSession(TenantName));
        }

        public IDocumentStore CurrentDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
                //Conventions =
                //{
                //    FindTypeTagName = type => typeof(ISiteInformation).IsAssignableFrom(type) ? typeof(ISiteInformation).Name : null
                //}
            }.Initialize();

            documentStore.DatabaseCommands.EnsureDatabaseExists(TenantName);

            return documentStore;
        }
    }
}