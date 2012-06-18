using System;
using ForeverRobot.Position.Infrastructure;

namespace ForeverRobot.Position.DropRobot
{
    public class RobotDroppedEvent : IRobotCommandEvent
    {
        public static RobotDroppedEvent Create(string robotName, double longitude, double latitude)
        {
            var robotDroppedEvent = new RobotDroppedEvent()
            {
                Id = Guid.NewGuid(),
                RobotName = robotName,
                Occurred = DateTime.Now,
                Longitude = longitude,
                Latitude = latitude
            };

            return robotDroppedEvent;
        }

        public Guid Id { get; set; }
        public string RobotName { get; set; }
        public DateTime Occurred { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }

    }

   
}