using System;
using ForeverRobot.Position.Infrastructure;

namespace ForeverRobot.Position.DisconnectRobot
{
    public class RobotDisconnectedEvent : IRobotCommandEvent
    {
        public static RobotDisconnectedEvent Create(string robotName, double latitude, double longitude)
        {
            var robotDisconnectedEvent = new RobotDisconnectedEvent()
            {
                Id = Guid.NewGuid(),
                RobotName = robotName,
                Occurred = DateTime.Now,
                Longitude = longitude,
                Latitude = latitude
            };

            return robotDisconnectedEvent;
        }

        public Guid Id { get; set; }
        public string RobotName { get; set; }
        public DateTime Occurred { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Online { get; set; }
    }
}