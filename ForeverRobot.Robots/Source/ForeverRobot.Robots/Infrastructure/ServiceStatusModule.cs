using Nancy;

namespace ForeverRobot.Robots.Infrastructure
{
    public class ServiceStatusModule : NancyModule
    {
        public ServiceStatusModule()
        {
            Get["/"] = _ => { return "Forever Robot - Robot service is up and running."; };
        }
    }
}