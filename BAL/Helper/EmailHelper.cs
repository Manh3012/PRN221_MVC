using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;

namespace BAL.Helpers {
    public class EmailHelper {
        public bool SendEmailTwoFactorCode(string userEmail, string code) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            try {
                //client.Send(mailMessage);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
                email.To.Add(MailboxAddress.Parse(userEmail));
                email.Subject = "Comfirm email";
                email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{code}</h1>" };

                // send email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex) {
                // log exception
            }
            return false;
        }

        public bool SendEmail(string userEmail, string confirmationLink) {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("learning.fpt.edu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Comfirm email";
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{confirmationLink}</h1>" };

            // send email
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("learning.fpt.edu@gmail.com", "vzddywkacpdhqtyi");
            smtp.Send(email);
            smtp.Disconnect(true);
            return false;
        }

        public bool SendEmailPasswordReset(string userEmail, string link) {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("care@yogihosting.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("care@yogihosting.com", "yourpassword");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex) {
                // log exception
            }
            return false;
        }
    }
}
