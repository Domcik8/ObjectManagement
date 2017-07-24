using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace ObjectManagement.Security
{
    public class SecurityManager
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[128 / 8];
            /* using (var rng = RandomNumberGenerator.Create())
             {
                 rng.GetBytes(salt);
             }*/

            for (int i = 0; i < 8; i++)
                salt[i] = (Byte)i;

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}