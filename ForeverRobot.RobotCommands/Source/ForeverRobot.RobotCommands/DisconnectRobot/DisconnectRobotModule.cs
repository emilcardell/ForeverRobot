using System;
using FluentValidation;
using Nancy;
using Nancy.ModelBinding;
using TinyHandler;

namespace ForeverRobot.RobotCommands.DisconnectRobot
{
    public class DisconnectRobotModule:NancyModule
    {
        public DisconnectRobotModule()
        {
            Post["/robot/command/disconnect/{robotname}"] = parameters =>
            {
                var inputModel = this.Bind<CreateDisconnectRobotInputModule>();
                inputModel.RobotName = parameters.robotname;

                var validationResult = new CreateDisconnectRobotInputModelValidator().Validate(inputModel);
                if (!validationResult.IsValid)
                    return HttpStatusCode.BadRequest;

                var robotMovedEvent = RobotDisconnectedEvent.Create(inputModel.RobotName,
                    inputModel.Longitude,
                    inputModel.Latitude);
                try
                {
                    HandlerCentral.Process(robotMovedEvent);
                }
                catch (Exception)
                {
                    return HttpStatusCode.BadRequest;
                }

                return HttpStatusCode.Created;
            };
        }
    }

    public class CreateDisconnectRobotInputModule
    {
        public string RobotName { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
    }


    public class CreateDisconnectRobotInputModelValidator : AbstractValidator<CreateDisconnectRobotInputModule>
    {
        public CreateDisconnectRobotInputModelValidator()
        {
            RuleFor(inputModel => inputModel.RobotName).NotEmpty();
            RuleFor(inputModel => inputModel.Longitude).NotEmpty();
            RuleFor(inputModel => inputModel.Latitude).NotEmpty();
        }
    }
}