using System;
using FluentValidation;
using Nancy;
using Nancy.ModelBinding;
using TinyHandler;

namespace ForeverRobot.Position.MoveRobot
{
    public class MoveRobotModule : NancyModule
    {
        public MoveRobotModule()
        {
            Post["/robot/command/move/{robotname}"] = parameters =>
            {
                var inputModel = this.Bind<CreateMoveRobotInputModule>();
                inputModel.RobotName = parameters.robotname;

                var validationResult = new CreateMoveRobotInputModelValidator().Validate(inputModel);
                if (!validationResult.IsValid)
                    return HttpStatusCode.BadRequest;

                var robotMovedEvent = RobotMovedEvent.Create(inputModel.RobotName,
                    inputModel.Longitude,
                    inputModel.Latitude);
                try
                {
                    HandlerCentral.Process(robotMovedEvent);
                }
                catch(Exception)
                {
                    return HttpStatusCode.BadRequest;
                }

                return HttpStatusCode.Created;
            };
        }

        public class CreateMoveRobotInputModule 
        {
            public string RobotName { get; set; }
            public Double Longitude { get; set; }
            public Double Latitude { get; set; }
        }


        public class CreateMoveRobotInputModelValidator : AbstractValidator<CreateMoveRobotInputModule>
        {
            public CreateMoveRobotInputModelValidator()
            {
                RuleFor(inputModel => inputModel.RobotName).NotEmpty();
                RuleFor(inputModel => inputModel.Longitude).NotEmpty();
                RuleFor(inputModel => inputModel.Latitude).NotEmpty();
            }
        }
    }
}