using Nancy.Bootstrappers.StructureMap;
using TinyHandler;

namespace ForeverRobot.BattleEngine.Infrastructure
{
    public class ApplicationBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Configure(x => x.AddRegistry<RavenDbBootstrapper>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotCreatedEvent>>().Use<RobotCreatedEventHandler>());
            HandlerCentral.Container = existingContainer;
        }
    }
}