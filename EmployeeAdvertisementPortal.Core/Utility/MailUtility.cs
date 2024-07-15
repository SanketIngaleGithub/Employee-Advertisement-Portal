using EmployeeAdvertisementPortal.Core.Constant;
using System;
using System.Net;
using System.Net.Mail;


namespace EmployeeAdvertisementPortal.Core.Utility
{
    public class MailUtility
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            string senderEmail = ConstantFile.senderMail; 
            string senderPassword = ConstantFile.senderPassword; 
            string smtpServer = ConstantFile.smtpServer;
            int smtpPort = ConstantFile.smtpPort;
            try
            {
                using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, body);
                    mailMessage.IsBodyHtml = true;

                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}