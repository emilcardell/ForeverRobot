using System;

namespace ForeverRobot.Users.Projections
{
    public class User
    {
        public string Id { get { return GetUserIdFromEmail(EMail); } }
        public string EMail { get; set; }
        public EncryptedPassword EncryptedPassword { get; set; }
        public DateTime Created { get; set; }

        public static string GetUserIdFromEmail(string eMail)
        {
            return "user/" + eMail.ToLowerInvariant();
        }
    }
}