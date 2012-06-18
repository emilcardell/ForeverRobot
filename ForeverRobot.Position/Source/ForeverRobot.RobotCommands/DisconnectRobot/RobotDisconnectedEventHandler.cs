using ForeverRobot.Position.Projections;
using Raven.Client;
using TinyHandler;

namespace ForeverRobot.Position.DisconnectRobot
{
    public class RobotDisconnectedEventHandler : HandlerModule<RobotDisconnectedEvent>
    {
        public RobotDisconnectedEventHandler()
        {
            
        }

         public RobotDisconnectedEventHandler(IDocumentStore documentStore)
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
                        Online = false
                    };

                    documentSession.Store(robotPosition);
                    documentSession.SaveChanges();
                }

            };
        }
    }
}