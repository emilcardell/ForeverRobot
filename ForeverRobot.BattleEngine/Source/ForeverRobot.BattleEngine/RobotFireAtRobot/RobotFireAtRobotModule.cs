using Nancy;

namespace ForeverRobot.BattleEngine.RobotFireAtRobot
{
    public class RobotFireAtRobotModule : NancyModule
    {
        public RobotFireAtRobotModule()
        {
            Post["RobotFireAtRobot"] = parameters =>
            {
                var command = new RobotFireAtRobotCommand();
                RobotFireAtRobotEngine.RobotFireAtRobotCommandQueue.Enqueue(command);
            };
        }
    }
}