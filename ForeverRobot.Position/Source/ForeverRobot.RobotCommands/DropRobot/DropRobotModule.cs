using System;

using FluentValidation;
using Nancy;
using Raven.Client;
using Nancy.ModelBinding;
using TinyHandler;

namespace ForeverRobot.Position.DropRobot
{
    public class DropRobotModule : NancyModule
    {
        public DropRobotModule()
        {
            Post["/robot/command/drop/{robotname}"] = parameters =>
            {
                var inputModel = this.Bind<CreateDropRobotInputModule>();
                inputModel.RobotName = parameters.robotname;

                var validationResult = new CreateRobotInputModelValidator().Validate(inputModel);
                if (!validationResult.IsValid)
                    return HttpStatusCode.BadRequest;

                var robotDroppedEvent = RobotDroppedEvent.Create(inputModel.RobotName,
                    inputModel.Longitude,
                    inputModel.Latitude);
                try
                {
                    HandlerCentral.Process(robotDroppedEvent);
                }
                catch(Exception)
                {
                    return HttpStatusCode.BadRequest;
                }

                return HttpStatusCode.Created;
            };
        }

        public class CreateDropRobotInputModule 
        {
            public string RobotName { get; set; }
            public Double Longitude { get; set; }
            public Double Latitude { get; set; }
        }


        public class CreateRobotInputModelValidator : AbstractValidator<CreateDropRobotInputModule>
        {
            public CreateRobotInputModelValidator()
            {
                RuleFor(inputModel => inputModel.RobotName).NotEmpty();
                RuleFor(inputModel => inputModel.Longitude).NotEmpty();
                RuleFor(inputModel => inputModel.Latitude).NotEmpty();
            }
        }

        
    }
}