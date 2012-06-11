using ForeverRobot.RobotCommands.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace ForeverRobot.RobotCommands.Specifications
{
    class when_a_robot_is_dropped
    {
        private static HttpStatusCode DropStatusCode;

        private Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            browser.Post("/robot/command/drop", with =>
            {
                with.HttpRequest();
                with.FormValue("RobotName", "yasuragi");
                with.FormValue("Longitude", "18,2435");
                with.FormValue("Latitude", "59,3472");
            });
        };
    }
}
