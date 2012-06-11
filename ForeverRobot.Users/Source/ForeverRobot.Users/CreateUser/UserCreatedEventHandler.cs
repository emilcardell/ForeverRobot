using System;
using ForeverRobot.Users.Projections;
using Raven.Client;
using TinyHandler;


namespace ForeverRobot.Users.CreateUser
{
    public class UserCreatedEventHandler : HandlerModule<UserCreatedEvent>
    {
        public UserCreatedEventHandler()
        {
            
        }
        public UserCreatedEventHandler(IDocumentStore documentStore)
        {
            Process = userCreatedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    var userResult = documentSession.Load<User>(User.GetUserIdFromEmail(userCreatedEvent.EMail));
                    if(userResult != null)
                        throw new UserAlreadyExistException();

                    documentSession.Store(userCreatedEvent);
                    documentSession.SaveChanges();
                }
            };

            Dispatch = userCreatedEvent =>
            {
                using (var documentSession = documentStore.OpenSession())
                {
                    var user = new User();
                    user.EMail = userCreatedEvent.EMail;
                    user.Created = userCreatedEvent.Created;
                    user.EncryptedPassword = userCreatedEvent.EncryptedPassword;

                    documentSession.Store(user);
                    documentSession.SaveChanges();
                }
            };
        }
    }

    public class UserAlreadyExistException : Exception
    {
    }
}