using ForeverRobot.Position.DisconnectRobot;
using ForeverRobot.Position.DropRobot;
using ForeverRobot.Position.MoveRobot;
using Nancy.Bootstrappers.StructureMap;
using TinyHandler;

namespace ForeverRobot.Position.Infrastructure
{
    public class ApplicationBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Configure(x => x.AddRegistry<RavenDbBootstrapper>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotDroppedEvent>>().Use<RobotDroppedEventHandler>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotMovedEvent>>().Use<RobotMovedEventHandler>());
            existingContainer.Configure(x => x.For<HandlerModule<RobotDisconnectedEvent>>().Use<RobotDisconnectedEventHandler>());
            HandlerCentral.Container = existingContainer;
        }
    }
}