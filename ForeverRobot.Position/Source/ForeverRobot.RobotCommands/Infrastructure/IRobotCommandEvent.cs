using System;

namespace ForeverRobot.Position.Infrastructure
{
    public interface IRobotCommandEvent
    {
        Guid Id { get; set; }
        string RobotName { get; set; }
        DateTime Occurred { get; set; }
        Double Longitude { get; set; }
        Double Latitude { get; set; }
    }
}