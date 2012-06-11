using System;
using FluentValidation;
using ForeverRobot.Users.Projections;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;

namespace ForeverRobot.Users.Login
{
    public class LoginModule : NancyModule
    {
        public LoginModule(IDocumentSession documentSession)
        {
            Post["/login"] = parameters =>
            {
                var inputModel = this.Bind<LoginInputModel>();
                new LoginInputModelValidator().ValidateAndThrow(inputModel);

                var userToLogin = documentSession.Load<User>(User.GetUserIdFromEmail(inputModel.EMail));
                
                if(userToLogin == null)
                    return HttpStatusCode.BadRequest;

                if(!userToLogin.EncryptedPassword.IsMatch(inputModel.Password))
                    return HttpStatusCode.BadRequest;

                var loginTolken = Guid.NewGuid();

                return Response.AsJson(new {loginTolken = loginTolken.ToString()});
            };
        }
    }

    public class LoginInputModel
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }

    public class LoginInputModelValidator : AbstractValidator<LoginInputModel>
    {
        public LoginInputModelValidator()
        {
            RuleFor(inputModel => inputModel.EMail).NotEmpty();
            RuleFor(inputModel => inputModel.Password).NotEmpty().Length(4, 1024);
        }
    }
}