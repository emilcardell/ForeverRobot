using ForeverRobot.RobotCommands.Projections;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using StructureMap.Configuration.DSL;

namespace ForeverRobot.RobotCommands.Infrastructure
{
    public class RavenDbBootstrapper : Registry
    {
        private const string TenantName = "Position";
        public RavenDbBootstrapper()
        {

            For<IDocumentStore>().Singleton().Use(CurrentDocumentStore());
            For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(x => x.GetInstance<IDocumentStore>().OpenSession());
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
            //IndexCreation.CreateIndexes(typeof(RobotPositions_ByNameAndLocation).Assembly, documentStore);
            

            

            return documentStore;
        }
    }
}