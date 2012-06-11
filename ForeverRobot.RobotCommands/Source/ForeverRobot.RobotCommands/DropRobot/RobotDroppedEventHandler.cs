using System;
using Raven.Client;
using TinyHandler;


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
        }
    }
}