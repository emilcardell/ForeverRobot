using System;
using FluentValidation;
using ForeverRobot.Users.Projections;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using TinyHandler;

namespace ForeverRobot.Users.CreateUser
{
    public class CreateUserModule : NancyModule
    {
        public CreateUserModule(IDocumentSession documentSession)
        {
            Post["/user/{email}"] = parameters =>
            {
                var inputModel = this.Bind<CreateUserInputModel>();
                inputModel.EMail = parameters.email;
                new CreateUserInputModelValidator().ValidateAndThrow(inputModel);

                var userCreatedEvent = UserCreatedEvent.Create(inputModel.EMail, inputModel.Password);
                try
                {
                    HandlerCentral.Process(userCreatedEvent);
                }
                catch (UserAlreadyExistException)
                {
                    return HttpStatusCode.BadRequest;
                }

                return HttpStatusCode.Created;
            };

            Get["/user/{email}"] = parameters =>
            {
                if (string.IsNullOrEmpty(parameters.email))
                    return HttpStatusCode.BadRequest;

                var userResult = documentSession.Load<User>(User.GetUserIdFromEmail(parameters.email));
                if (userResult == null)
                    return HttpStatusCode.NotFound;

                return Response.AsJson(userResult as object);
            };
        }
    }

    public class CreateUserInputModel
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserInputModelValidator()
        {
            RuleFor(inputModel => inputModel.EMail).NotEmpty().EmailAddress();
            RuleFor(inputModel => inputModel.Password).NotEmpty().Length(4, 1024);
        }
    }


}