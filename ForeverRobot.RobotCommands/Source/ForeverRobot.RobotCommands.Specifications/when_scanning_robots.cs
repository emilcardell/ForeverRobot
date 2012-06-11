using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForeverRobot.RobotCommands.Projections;
using ForeverRobot.RobotCommands.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy.Testing;

namespace ForeverRobot.RobotCommands.Specifications
{
    class when_scanning_robots
    {
        private static List<RobotPosition> DetectedRobots;

        private Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            browser.Post("/robot/command/drop/yasuragi", with =>
            {
                with.HttpRequest();
                with.FormValue("Longitude", "18,2435");
                with.FormValue("Latitude", "59,3472");
            });
        
            browser.Post("/robot/command/drop/lidingo", with =>
            {
                with.HttpRequest();
                with.FormValue("Longitude", "20,2435");
                with.FormValue("Latitude", "60,3472");
            });

            var getResult = browser.Get("/scanRobots/", with =>
            {
              with.HttpRequest();
              with.FormValue("robotName", "yasuragi");
              with.FormValue("longitude", "18,2435");
              with.FormValue("latitude", "59,3472");
            });

            DetectedRobots = getResult.Body.DeserializeJson<List<RobotPosition>>();
        };
    }
}
