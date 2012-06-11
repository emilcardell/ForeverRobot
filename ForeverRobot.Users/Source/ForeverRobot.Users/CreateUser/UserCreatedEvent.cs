using System;

namespace ForeverRobot.Users.CreateUser
{
    public class UserCreatedEvent
    {
        public static UserCreatedEvent Create(string email, string password)
        {
            var userCreatedEvent = new UserCreatedEvent()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                EMail = email,
                EncryptedPassword = EncryptedPassword.Create(password)

            };

            return userCreatedEvent;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string EMail { get; set; }
        public EncryptedPassword EncryptedPassword { get; set; }
    }
}