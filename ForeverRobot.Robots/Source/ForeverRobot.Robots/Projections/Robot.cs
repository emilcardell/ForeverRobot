using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForeverRobot.Robots.Projections
{
    public class Robot
    {
        public string Id { get { return GetRobotIdFromName(Name); } }
        public string Name { get; set; }
        public string ClientType { get; set; }
        public DateTime Created { get; set; }

        public static string GetRobotIdFromName(string name)
        {
            return "robot/" + name.ToLowerInvariant();
        }
    }
}