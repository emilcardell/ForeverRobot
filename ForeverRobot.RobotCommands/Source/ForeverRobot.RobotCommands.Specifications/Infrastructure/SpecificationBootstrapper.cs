
using ForeverRobot.RobotCommands.DisconnectRobot;
using ForeverRobot.RobotCommands.Infrastructure;
using ForeverRobot.RobotCommands.MoveRobot;
using ForeverRobot.RobotCommands.DropRobot;
using ForeverRobot.RobotCommands.Projections;
using Nancy.Testing.Fakes;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using TinyHandler;

namespace ForeverRobot.RobotCommands.Specifications.Infrastructure
{
    public class SpecificationBootstrapper : DiscoveryBootstrapper
    {
        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Configure(x => x.SelectConstructor(() => new FakeNancyModule()));
            existingContainer.Configure(x => x.For<IDocumentStore>().Singleton().Use(GetEmbededInMemoryStore));
            existingContainer.Configure(x => x.For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(y => y.GetInstance<IDocumentStore>().OpenSession()));
            existingContainer.Configure(x => x.For<HandlerModule<RobotDroppedEvent>>().Use<RobotDroppedEventHandler>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotMovedEvent>>().Use<RobotMovedEventHandler>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotDisconnectedEvent>>().Use<RobotDisconnectedEventHandler>());
            HandlerCentral.Container = existingContainer;
            
        }

        public IDocumentStore GetEmbededInMemoryStore()
        {
            var documentStore = new EmbeddableDocumentStore() { RunInMemory = true }.Initialize();
            IndexCreation.CreateIndexes(typeof(RobotPositions_ByNameAndLocation).Assembly, documentStore);
            return documentStore;
        }
    }
}
