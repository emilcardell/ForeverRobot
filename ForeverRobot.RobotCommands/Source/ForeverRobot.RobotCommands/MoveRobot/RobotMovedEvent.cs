using System;
using ForeverRobot.RobotCommands.Infrastructure;

namespace ForeverRobot.RobotCommands.MoveRobot
{
    public class RobotMovedEvent: IRobotCommandEvent
    {
        public static RobotMovedEvent Create(string robotName, double longitude, double latitude)
        {
            var robotMovedEvent = new RobotMovedEvent()
            {
                Id = Guid.NewGuid(),
                RobotName = robotName,
                Occurred = DateTime.Now,
                Longitude = longitude,
                Latitude = latitude
            };

            return robotMovedEvent;
        }

        public Guid Id { get; set; }
        public string RobotName { get; set; }
        public DateTime Occurred { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}