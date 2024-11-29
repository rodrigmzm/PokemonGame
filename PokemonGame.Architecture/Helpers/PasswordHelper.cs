using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PokemonGame.Architecture.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256()) 
            { 
                var salt = Guid.NewGuid().ToString();
                var saltedPassword = salt + password;
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));
                return $"{salt}:{hash}";
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Invalid password format");

            var salt = parts[0];
            var hash = parts[1];
            using (var hmac = new HMACSHA256())
            { 
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(salt + password)));
                return hash == computedHash;
            }  
        }
    }
}
