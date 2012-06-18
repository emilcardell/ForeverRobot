using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;

namespace ForeverRobot.Position.Projections
{
    public class ScanRobotsModule : NancyModule 
    {
      

        public ScanRobotsModule(IDocumentSession documentSession)
        {
            Get["/scanRobots/"] = parameters =>
            {
                var inputModel = this.Bind<CreateScanRobotsInputModule>();

                var robotsScanResult =
                    documentSession.Advanced.LuceneQuery<RobotPosition>("RobotPositions/ByNameAndLocation")
                        .WhereEquals("Online", true)
                        .WhereGreaterThan("LastUpdate", DateTime.Now.AddMinutes(-1))
                        .WithinRadiusOf(radius: 10, latitude: inputModel.Latitude, longitude: inputModel.Longitude)
                        .ToList();

                return Response.AsJson(robotsScanResult.Where(r => r.RobotName != inputModel.RobotName).ToArray());
            };

            Get["/scanAllRobots/"] = parameters =>
            {
                var robotsScanResult =
                    documentSession.Query<RobotPosition>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).Where(
                        r => r.Online && r.LastUpdate > DateTime.Now.AddMinutes(-1)).ToList();

                var response = Response.AsJson(robotsScanResult.ToArray());
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                return response;
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