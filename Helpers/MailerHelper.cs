using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntoSport.Models;
namespace IntoSport.Helpers
{
    public class MailerHelper
    {
        public MailerHelper(String body, String subject,IntoSport.Models.User user)
        { 
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(user.email);
            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress("13134027@student.hhs.nl");
            message.Body = body;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("outlook.office365.com");
            smtp.Send(message);
        }
    }
}