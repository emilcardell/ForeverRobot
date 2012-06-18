using ForeverRobot.Position.Projections;
using ForeverRobot.Position.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace ForeverRobot.Position.Specifications
{
    class when_a_robot_is_dropped
    {
        private static HttpStatusCode DropStatusCode;

        private static RobotPosition RobotPositionResult;

        private Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            var dropResult = browser.Post("/robot/command/drop/yasuragi", with =>
            {
                with.HttpRequest();
                with.FormValue("Longitude", "18,2435");
                with.FormValue("Latitude", "59,3472");
            });

            DropStatusCode = dropResult.StatusCode;

            var getResult = browser.Get("/robotposition/yasuragi", with =>
            {
                with.HttpRequest();
            });

            RobotPositionResult = getResult.Body.DeserializeJson<RobotPosition>();


        };


        private It should_exist = () => DropStatusCode.Equals(HttpStatusCode.OK);

        private It should_be_the_same_latitude = () => RobotPositionResult.Latitude.Equals("59,3472");
        private It should_be_the_same_longitude = () => RobotPositionResult.Longitude.Equals("18,2435");
        private It should_be_online = () => RobotPositionResult.Online.Equals(true);
    }
}
