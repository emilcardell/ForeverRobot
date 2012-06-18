using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Raven.Client;

namespace ForeverRobot.Position.Projections
{
    public class RobotPositionModule : NancyModule
    {
        public RobotPositionModule(IDocumentSession documentSession)
        {
            Get["/robotPosition/{robotname}"] = parameters =>
            {
                if (string.IsNullOrWhiteSpace(parameters.robotname))
                    return HttpStatusCode.BadRequest;

                var robotPositionResult = documentSession.Load<RobotPosition>(RobotPosition.GetRobotPositionIdFromName(parameters.robotname));
                if (robotPositionResult == null)
                    return HttpStatusCode.BadRequest;

                return Response.AsJson(robotPositionResult as object);
            };
        }
    }
}