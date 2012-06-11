using ForeverRobot.RobotCommands.Projections;
using Raven.Client;
using TinyHandler;

namespace ForeverRobot.RobotCommands.MoveRobot
{
    public class RobotMovedEventHandler : HandlerModule<RobotMovedEvent>
    {
         public RobotMovedEventHandler()
        {
            
        }

         public RobotMovedEventHandler(IDocumentStore documentStore)
        {
            Process = robotMovedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    documentSession.Store(robotMovedEvent);
                    documentSession.SaveChanges();
                }
            };
             
            Dispatch = robotMovedEvent =>
            {

                using (var documentSession = documentStore.OpenSession())
                {
                    var robotPosition = new RobotPosition()
                    {
                        RobotName = robotMovedEvent.RobotName,
                        LastUpdate = robotMovedEvent.Occurred,
                        Latitude = robotMovedEvent.Latitude,
                        Longitude = robotMovedEvent.Longitude,
                        Online = true
                    };

                    documentSession.Store(robotPosition);
                    documentSession.SaveChanges();
                }

            };
        }
    }
}