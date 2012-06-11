using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;

namespace ForeverRobot.RobotCommands.Projections
{
    public class RobotPositions_ByNameAndLocation : AbstractIndexCreationTask<RobotPosition>
    {
        public RobotPositions_ByNameAndLocation()
        {
            Map = robotPosition => from r in robotPosition
                                 select new {r.RobotName, r.Online, LatestUpdate = r.LastUpdate , _ = SpatialIndex.Generate(r.Latitude, r.Longitude)};
        }
    }
}