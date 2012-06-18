using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ForeverRobot.Position.Projections;
using ForeverRobot.Position.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace ForeverRobot.Position.Specifications
{
    class when_scanning_robots
    {
        private static HttpStatusCode Robot1DropStatusCode;
        private static HttpStatusCode Robot2DropStatusCode;
        private static List<RobotPosition> DetectedRobots;

        private Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            var robot1DropResult = browser.Post("/robot/command/drop/yasuragi", with =>
            {
                with.HttpRequest();
                
                with.FormValue("Longitude", "18,2435");
                with.FormValue("Latitude", "59,3472");
            });

            Robot1DropStatusCode = robot1DropResult.StatusCode;
        
            var robot2DropResult = browser.Post("/robot/command/drop/lidingo", with =>
            {
                with.HttpRequest();
                with.FormValue("Longitude", "20,2435");
                with.FormValue("Latitude", "60,3472");
            });

            Robot2DropStatusCode = robot2DropResult.StatusCode;
            Thread.Sleep(1000);
            var getResult = browser.Get("/scanAllRobots/", with =>
            {
              with.HttpRequest();
              with.FormValue("RobotName", "yasuragi");
              with.FormValue("Longitude", "18,2435");
              with.FormValue("Latitude", "59,3472");
            });

            DetectedRobots = getResult.Body.DeserializeJson<List<RobotPosition>>();
        };

        private It should_have_dropped_robot1 = () => Robot1DropStatusCode.Equals(HttpStatusCode.OK);
        private It should_have_dropped_robot2 = () => Robot2DropStatusCode.Equals(HttpStatusCode.OK);
        private It should_find_a_robot = () => DetectedRobots.Count().ShouldBeGreaterThan(0);
        
    }
        

      
}
