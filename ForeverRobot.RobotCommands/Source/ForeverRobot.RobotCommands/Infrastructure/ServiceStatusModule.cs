using Nancy;

namespace ForeverRobot.RobotCommands.Infrastructure
{
    public class ServiceStatusModule : NancyModule
    {
        public ServiceStatusModule()
        {
            Get["/"] = _ => { return "Forever Robot - Position service is up and running."; };
        }
    }
}