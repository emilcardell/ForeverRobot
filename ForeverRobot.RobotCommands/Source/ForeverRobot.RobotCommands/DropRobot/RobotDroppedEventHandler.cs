using System;
using ForeverRobot.RobotCommands.Projections;
using Raven.Client;
using TinyHandler;
using ForeverRobot.RobotCommands.Projections;


namespace ForeverRobot.RobotCommands.DropRobot
{
    public class RobotDroppedEventHandler: HandlerModule<RobotDroppedEvent>
    {

        public RobotDroppedEventHandler()
        {
            
        }

        public RobotDroppedEventHandler(IDocumentStore documentStore)
        {
            Process = robotDroppedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    documentSession.Store(robotDroppedEvent);
                    documentSession.SaveChanges();
                }
            };

            Dispatch = robotDroppedEvent =>
            {

                using (var documentSession = documentStore.OpenSession())
                {
                    var robotPosition = new RobotPosition()
                    {
                        RobotName = robotDroppedEvent.RobotName,
                        LastUpdate = robotDroppedEvent.Occurred,
                        Latitude = robotDroppedEvent.Latitude,
                        Longitude = robotDroppedEvent.Longitude,
                        Online = true
                    };

                    documentSession.Store(robotPosition);
                    documentSession.SaveChanges();
                }

            };
        }
    }
}