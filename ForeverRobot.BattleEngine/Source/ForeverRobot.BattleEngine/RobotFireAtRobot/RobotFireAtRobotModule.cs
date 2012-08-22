using Nancy;
using Nancy.ModelBinding;

namespace ForeverRobot.BattleEngine.RobotFireAtRobot
{
    public class RobotFireAtRobotModule : NancyModule
    {
        public RobotFireAtRobotModule()
        {
            Post["RobotFireAtRobot"] = parameters =>
            {
                var command = this.Bind<RobotFireAtRobotCommand>();
                RobotFireAtRobotEngine.RobotFireAtRobotCommandQueue.Enqueue(command);
            };
        }
    }
}