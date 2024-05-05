using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.SecurityLogic
{
    public static class EncryptPassword
    {

        public static (string Salt, string HashPassword) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // 128-bit salt
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(20); // 160-bit hash

                    
                    string base64Salt = Convert.ToBase64String(salt);
                    string base64Hash = Convert.ToBase64String(hash);

                    return (base64Salt, base64Hash);
                }
            }
        }

        public static bool VerifyPassword(string password, string storedBase64Salt, string storedBase64Hash)
        {
           
            byte[] storedSalt = Convert.FromBase64String(storedBase64Salt);
            byte[] storedHash = Convert.FromBase64String(storedBase64Hash);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                {
                    if (storedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }


    }
}
