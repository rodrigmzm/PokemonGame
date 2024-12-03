using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PokemonGame.Repository.Services
{
    public class AccountService
    {
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        //public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        //{
        //    using (var hmac = new HMACSHA256())
        //    {
        //        var saltedPassword = storedSalt + enteredPassword;
        //        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));

        //        return hash == storedHash;
        //    }
        //}
    }
}
