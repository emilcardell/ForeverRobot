using Nancy;

namespace ForeverRobot.BattleEngine.Infrastructure
{
    public class ServiceStatusModule : NancyModule
    {
        public ServiceStatusModule()
        {
            Get["/"] = _ => { return "Forever Robot - Battle Engine service is up and running."; };
        }
    }
}