using ForeverRobot.Robots.CreateRobot;
using ForeverRobot.Robots.Infrastructure;
using Nancy.Bootstrappers.StructureMap;
using TinyHandler;

namespace ForeverRobot.Robots.Infrastructure
{
    public class ApplicationBootstrapper : DiscoveryBootstrapper
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