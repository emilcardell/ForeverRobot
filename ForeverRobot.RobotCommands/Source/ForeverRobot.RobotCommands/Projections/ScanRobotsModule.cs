using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;

namespace ForeverRobot.RobotCommands.Projections
{
    public class ScanRobotsModule : NancyModule 
    {
        public ScanRobotsModule(IDocumentSession documentSession)
        {
            Get["/scanRobots/"] = parameters =>
            {
                var inputModel = this.Bind<CreateScanRobotsInputModule>();

                var robotsScanResult =
                    documentSession.Advanced.LuceneQuery<RobotPosition>("RobotLocations/ByNameAndPosition")
                        .WhereEquals("Online", true)
                        .WhereGreaterThan("LastUpdate", DateTime.Now.AddMinutes(-1))
                        .WithinRadiusOf(radius: 2, latitude: inputModel.Latitude, longitude: inputModel.Longitude)
                        .ToList();

                return Response.AsJson(robotsScanResult.Where(r => r.RobotName != inputModel.RobotName).ToArray());
            };
        }
    }

    public class CreateScanRobotsInputModule
    {
        public string RobotName { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
    }
}