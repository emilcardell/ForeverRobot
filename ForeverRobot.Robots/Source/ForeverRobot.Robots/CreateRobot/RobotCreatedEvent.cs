using System;


namespace ForeverRobot.Robots.CreateRobot
{
    public class RobotCreatedEvent
    {
        public static RobotCreatedEvent Create(string name, string clientType)
        {
            var robotCreatedEvent = new RobotCreatedEvent()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = name,
                ClientType = clientType
            };

            return robotCreatedEvent;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }


    }
}