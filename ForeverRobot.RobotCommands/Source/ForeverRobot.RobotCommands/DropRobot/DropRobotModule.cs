using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Nancy;
using Raven.Client;

namespace ForeverRobot.RobotCommands.DropRobot
{
    public class DropRobotModule : NancyModule
    {
        public DropRobotModule(IDocumentSession documentSession)
        {
            //Post["/robot/command/{name}"] = parameters =>
            //                                    {

            //                                        var robotDroppedEvent = RobotDroppedEvent.Create();
            //                                    };
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
                //RuleFor(inputModel => inputModel.Password).NotEmpty().Length(4, 1024);
            }
        }

        
    }
}