using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserUtil
    {
        private const int MAX_SIZE = 8; // randow password 8 characters
        public static string HashPassword(string password)
        {
            // using SHA256 for hash password
            byte[] hashPw = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            string result = "";
            for (int i = 0; i < hashPw.Length; i++)
            {
                // convert into hexadecimal
                result = result + hashPw[i].ToString("x2");
            }
            return result;
        }
        public static string RandomPassword()
        {
            Random random = new Random();
            // password contain both alphabets and numbers
            string format = "abcdefghijklmnopqrstuvwxyz0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
            string result = "";
            for (int i = 0; i < MAX_SIZE; i++)
            {
                // get random index character
                int index = random.Next(format.Length);
                result = result + format[index];
            }
            return result;
        }

    }
}
