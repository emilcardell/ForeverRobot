using System.Collections.Generic;

namespace ForeverRobot.BattleEngine.RobotFireAtRobot
{
    public class RobotFireAtRobotEngine
    {
        public static Queue<RobotFireAtRobotCommand> RobotFireAtRobotCommandQueue = new Queue<RobotFireAtRobotCommand>();

        public static void StartEngine()
        {
            while (true)
            {
                var commandToProcess = RobotFireAtRobotCommandQueue.Dequeue();

                //Calclulate


            }
        }
    }
}