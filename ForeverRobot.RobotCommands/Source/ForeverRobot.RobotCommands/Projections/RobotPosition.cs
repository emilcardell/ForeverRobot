using System;

namespace ForeverRobot.RobotCommands.Projections
{
    public class RobotPosition
    {
        public string Id { get { return GetRobotPositionIdFromName(RobotName); } }
        public string RobotName { get; set; }
        public DateTime LastUpdate { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
        public bool Online { get; set; }

        public static string GetRobotPositionIdFromName(string robotName)
        {
            return "robotPosition/" + robotName.ToLowerInvariant();
        }
    }
}