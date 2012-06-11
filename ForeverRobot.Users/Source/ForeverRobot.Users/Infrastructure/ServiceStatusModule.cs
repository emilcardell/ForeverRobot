using Nancy;

namespace ForeverRobot.Users.Infrastructure
{
    public class ServiceStatusModule : NancyModule
    {
        public ServiceStatusModule()
        {
            Get["/"] = _ => { return "Forever Robot - User service is up and running."; };
        }
    }
}