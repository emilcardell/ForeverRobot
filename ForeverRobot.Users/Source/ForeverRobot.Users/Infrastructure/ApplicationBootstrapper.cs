using ForeverRobot.Users.CreateUser;
using Nancy.Bootstrappers.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using TinyHandler;

namespace ForeverRobot.Users.Infrastructure
{
    public class ApplicationBootstrapper : DiscoveryBootstrapper
    {
        private const string TenantName = "User";

        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Configure(x => x.For<IDocumentStore>().Singleton().Use(CurrentDocumentStore()));
            existingContainer.Configure(x => x.For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(y => y.GetInstance<IDocumentStore>().OpenSession()));
            existingContainer.Configure(x => x.For<HandlerModule<UserCreatedEvent>>().Use<UserCreatedEventHandler>());
            HandlerCentral.Container = existingContainer;
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