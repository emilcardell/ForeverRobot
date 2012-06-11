
using ForeverRobot.RobotCommands.Infrastructure;
using Nancy.Testing.Fakes;
using Raven.Client;
using Raven.Client.Embedded;
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
            existingContainer.Configure(x => x.For<HandlerModule<DropRobot.RobotDroppedEvent>>().Use<DropRobot.RobotDroppedEventHandler>());
            HandlerCentral.Container = existingContainer;
            
        }

        public IDocumentStore GetEmbededInMemoryStore()
        {
            return new EmbeddableDocumentStore() { RunInMemory = true }.Initialize();
        }
    }
}
