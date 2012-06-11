using System;
using FluentValidation;
using ForeverRobot.Robots.Projections;
using Nancy;
using Nancy.ModelBinding;
using TinyHandler;
using Raven.Client;

namespace ForeverRobot.Robots.CreateRobot
{
    public class CreateRobotModule : NancyModule
    {
        public CreateRobotModule(IDocumentSession documentSession)
        {
            Put["/robot/{name}"] = parameters =>
            {
                var robotResult = documentSession.Load<Robot>(Robot.GetRobotIdFromName(parameters.name));
                if (robotResult != null)
                    return HttpStatusCode.BadRequest;

                var inputModel = this.Bind<CreateRobotInputModel>();
                inputModel.Name = parameters.name;

                var result = new CreateRobotInputModelValidator().Validate(inputModel);
                if (!result.IsValid)
                    return HttpStatusCode.BadRequest;


                var robotCreatedEvent = RobotCreatedEvent.Create(inputModel.Name, inputModel.ClientType);
                try
                {
                    HandlerCentral.Process(robotCreatedEvent);
                }
                catch (Exception)
                {
                    return HttpStatusCode.BadRequest;
                }
                                
                return HttpStatusCode.Created;
            };
        }
    }

    public class CreateRobotInputModel
    {
        public string Name { get; set; }
        public string ClientType { get; set; }
    }

    public class CreateRobotInputModelValidator : AbstractValidator<CreateRobotInputModel>
    {
        public CreateRobotInputModelValidator()
        {
            RuleFor(inputModel => inputModel.Name).NotEmpty();
            //RuleFor(inputModel => inputModel.Password).NotEmpty().Length(4, 1024);
        }
    }



}