using ForeverRobot.Robot.Specification;
using ForeverRobot.Robots.Specifications.Infrastructure;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace ForeverRobot.Robots.Specification
{
    public class when_a_robot_is_created
    {
        private static HttpStatusCode CreateStatusCode;
        private static HttpStatusCode RecreateStatusCode;
        private static Projections.Robot RobotResult;


        Establish context = () =>
        {
            var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            browser.Post("/robot/lasse", with =>
            {
                with.HttpRequest();
                with.FormValue("ClientType", "mSpec");
            });

            var recreateResult = browser.Post("/robot/lasse", with =>
            {
                with.HttpRequest();
                with.FormValue("ClientType", "mSpec");
            });

            RecreateStatusCode = recreateResult.StatusCode;

            var getResult = browser.Get("/robot/lasse", with =>
            {
                with.HttpRequest();                
            });

            CreateStatusCode = getResult.StatusCode;
            RobotResult = getResult.Body.DeserializeJson<Projections.Robot>();


        };

        private It should_exist = () => CreateStatusCode.ShouldEqual(HttpStatusCode.OK);

        private It should_not_be_able_to_be_created_again = () => RecreateStatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private It should_be_the_same_client_type_as_inputed = () => RobotResult.ClientType.ShouldEqual("mSpec");
    }

}
