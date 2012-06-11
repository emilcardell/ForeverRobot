using ForeverRobot.Users.CreateUser;
using Nancy.Bootstrappers.StructureMap;
using TinyHandler;

namespace ForeverRobot.Users.Infrastructure
{
    public class ApplicationBootstrapper : DiscoveryBootstrapper
    {
        protected override void ConfigureApplicationContainer(StructureMap.IContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Configure(x => x.AddRegistry<RavenDbBootstrapper>());
            existingContainer.Configure(x => x.For<HandlerModule<UserCreatedEvent>>().Use<UserCreatedEventHandler>());
            HandlerCentral.Container = existingContainer;
        }
    }
}