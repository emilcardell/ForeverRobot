using System;
using ForeverRobot.Robots.Projections;
using Raven.Client;
using TinyHandler;

namespace ForeverRobot.Robots.CreateRobot
{
    public class RobotCreatedEventHandler : HandlerModule<RobotCreatedEvent>
    {
        public RobotCreatedEventHandler()
        {

        }

        public RobotCreatedEventHandler(IDocumentStore documentStore)
        {
            Process = robotCreatedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    var robotResult =
                        documentSession.Load<Robot>(Robot.GetRobotIdFromName(robotCreatedEvent.Name));
                    if (robotResult != null)
                        throw new RobotAlreadyExistException();
   
                    documentSession.Store(robotCreatedEvent);
                    documentSession.SaveChanges();
                }
            };

            Dispatch = robotCreatedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    var robot = new Robot
                    {
                        Name = robotCreatedEvent.Name,
                        ClientType = robotCreatedEvent.ClientType,
                        Created = robotCreatedEvent.Created,
                        OwnerEMail = robotCreatedEvent.OwnerEMail
                    };

                    documentSession.Store(robot);
                    documentSession.SaveChanges();
                }

            };
        }
    }

    public class RobotAlreadyExistException: Exception
    {
        
    }
}