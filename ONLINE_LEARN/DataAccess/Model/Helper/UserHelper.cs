using Common.Enums;
using DataAccess.Extensions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Model.Helper
{
    public class UserHelper
    {
        private const int MAX_SIZE = 8; // randow password 8 characters

        public static List<string> Genders { get; } = new List<string>()
        {
            UserInfo.Gender_Male.getDescription(),
            UserInfo.Gender_Female.getDescription(),
            UserInfo.Gender_Other.getDescription()
        };

        public static string HashPassword(string password)
        {
            // using SHA256 for hash password
            byte[] hashPw = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashPw.Length; i++)
            {
                // convert into hexadecimal
                builder.Append(hashPw[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string RandomPassword()
        {
            Random random = new Random();
            // password contain both alphabets and numbers
            string format = "abcdefghijklmnopqrstuvwxyz0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < MAX_SIZE; i++)
            {
                // get random index character
                int index = random.Next(format.Length);
                builder.Append(format[index]);
            }
            return builder.ToString();
        }

        public static string BodyEmailForRegister(string password)
        {
            string body = "<h1>Mật khẩu cho tài khoản mới</h1>\n" +
                            "<p>Mật khẩu của bạn là: " + password + "</p>\n";
            return body;
        }

        public static Task sendEmail(string subject, string body, string to)
        {
            // get information of mail address
            ConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot config = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            IConfigurationSection mail = config.GetSection("MailAddress");
            // create message to send
            MimeMessage mime = new MimeMessage();
            MailboxAddress mailFrom = MailboxAddress.Parse(mail["Username"]);
            MailboxAddress mailTo = MailboxAddress.Parse(to);
            mime.From.Add(mailFrom);
            mime.To.Add(mailTo);
            mime.Subject = subject;
            mime.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            // send message
            SmtpClient smtp = new SmtpClient();
            smtp.Connect(mail["Host"]);
            smtp.Authenticate(mail["Username"], mail["Password"]);
            smtp.Send(mime);
            smtp.Disconnect(true);
            return Task.CompletedTask;
        }

        public static string BodyEmailForForgetPassword(string password)
        {
            string body = "<h1>Mật khẩu mới</h1>\n" +
                            "<p>Mật khẩu mới là: " + password + "</p>\n" +
                            "<p>Không nên chia sẻ mật khẩu của bạn với người khác.</p>";
            return body;
        }
    }
}
