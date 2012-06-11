using Machine.Specifications;
using Nancy;
using Nancy.Testing;
using Raven.Client;
using StructureMap;

namespace ForeverRobot.Users.Specification
{
    public class when_a_user_is_created
    {
        private static HttpStatusCode LoginStatusCode;
        private static HttpStatusCode RecreateStatusCode;

        Establish context = () =>
        {
             var bootstrapper = new SpecificationBootstrapper();
            var browser = new Browser(bootstrapper);

            browser.Put("/User/user@host.com", with =>
            {
                with.HttpRequest();
                with.FormValue("password", "firstPassword");
            });

            var loginResult = browser.Post("/login", with =>
            {
                with.HttpRequest();
                with.FormValue("email", "user@host.com");
                with.FormValue("password", "firstPassword");
            });

            LoginStatusCode = loginResult.StatusCode;


            browser.Put("/User/user@host.com", with =>
            {
                with.HttpRequest();
                with.FormValue("password", "secondPassword");
            });

            var loginRecreateResult = browser.Post("/login", with =>
            {
                with.FormValue("email", "user@host.com");
                with.FormValue("password", "secondPassword");
                with.HttpRequest();
            });

            RecreateStatusCode = loginRecreateResult.StatusCode;

        };

        private It should_be_able_to_login_in = () => LoginStatusCode.ShouldEqual(HttpStatusCode.OK);

        private It should_not_be_able_to_be_created_again = () => RecreateStatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }

}
