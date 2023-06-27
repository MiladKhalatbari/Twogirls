using System.Net.Mail;

namespace TwoGirls.Core.Senders
{

    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            var mail = new MailMessage();
            var SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("aqamiladam@gmail.com", "TwoGirls");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("twogirls.helpdesk@gmail.com", "tvriyrcywewyyzpj");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }

}
