using System;
using System.Linq;
using System.Security.Cryptography;

namespace ForeverRobot.Users
{
    public sealed class EncryptedPassword
    {
        private const int HashSize = 20;
        private const int SaltSize = 16;
        private const int NumberOfIterations = 1000;

        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public int NumberOfIteration { get; set; }

        public bool IsMatch(string passwordToMatch)
        {
            return PasswordHash.SequenceEqual(new Rfc2898DeriveBytes(passwordToMatch, Salt, NumberOfIteration).GetBytes(HashSize));
        }


        public static EncryptedPassword Create(string textPassword)
        {
            if(string.IsNullOrEmpty(textPassword))
            {
                throw new ApplicationException("Password can't be null or empty.");
            }
            var encryptedPassword = new EncryptedPassword();
            encryptedPassword.Salt = GetRandomSaltKey();
            encryptedPassword.NumberOfIteration = NumberOfIterations;
            encryptedPassword.PasswordHash = new Rfc2898DeriveBytes(textPassword, encryptedPassword.Salt, encryptedPassword.NumberOfIteration).GetBytes(HashSize);

            return encryptedPassword;
        }
        
        static byte[] GetRandomSaltKey()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            return salt;
        }
    }
}