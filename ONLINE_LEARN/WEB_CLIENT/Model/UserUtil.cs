﻿using DataAccess.Const;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WEB_CLIENT.Model
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

        public static List<string> getAllGender()
        {
            List<string> list = new List<string>();
            list.Add(UserConst.GENDER_MALE);
            list.Add(UserConst.GENDER_FEMALE);
            list.Add(UserConst.GENDER_OTHER);
            return list;
        }
    }
}
