using ForeverRobot.RobotCommands.Projections;
using ForeverRobot.RobotCommands.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace ForeverRobot.RobotCommands.Specifications
{
    class when_a_robot_is_moved
    {
        private static HttpStatusCode MoveStatusCode;

        private static RobotPosition RobotPositionResult;

        private Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            var moveResult = browser.Post("/robot/command/move/yasuragi", with =>
            {
                with.HttpRequest();
                with.FormValue("Longitude", "18,2436");
                with.FormValue("Latitude", "59,3475");
            });

            MoveStatusCode = moveResult.StatusCode;

            var getResult = browser.Get("/robotposition/yasuragi", with =>
            {
                with.HttpRequest();
            });

            RobotPositionResult = getResult.Body.DeserializeJson<RobotPosition>();


        };


        private It should_exist = () => MoveStatusCode.Equals(HttpStatusCode.OK);

        private It should_be_the_same_latitude = () => RobotPositionResult.Latitude.Equals("59,3475");
        private It should_be_the_same_longitude = () => RobotPositionResult.Longitude.Equals("18,2436");
        private It should_be_online = () => RobotPositionResult.Online.Equals(true);
    }
}
