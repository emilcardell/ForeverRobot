using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Raven.Client;

namespace ForeverRobot.Robots.Projections
{
    public class RobotProjectionModule :NancyModule
    {
        public RobotProjectionModule(IDocumentSession documentSession)
        {
            Get["/robot/{name}"] = parameters =>
            {
                if (string.IsNullOrWhiteSpace(parameters.name))
                    return HttpStatusCode.BadRequest;

                var robotResult = documentSession.Load<Robot>(Robot.GetRobotIdFromName(parameters.name));
                if (robotResult == null)
                    return HttpStatusCode.NotFound;

                return Response.AsJson(robotResult as object);
            };

            Get["/robot/byowner/{email}"] = parameters =>
            {
                if (string.IsNullOrWhiteSpace(parameters.email))
                    return HttpStatusCode.BadRequest;

                string email = parameters.email;
                var robotResult = documentSession.Query<Robot>().FirstOrDefault(x => x.OwnerEMail.Equals(email, StringComparison.CurrentCultureIgnoreCase));
                if (robotResult == null)
                    return HttpStatusCode.NotFound;

                return Response.AsJson(robotResult as object);
            };
        }
    }
}