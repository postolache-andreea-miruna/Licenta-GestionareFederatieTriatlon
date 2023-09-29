using GestionareFederatieTriatlon.Configuratii;
using GestionareFederatieTriatlon.Modele;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace GestionareFederatieTriatlon.Manageri
{
    public class EmailServ: IEmailServ
    {
        EmailSetari emailSetari = null;

        public EmailServ(IOptions<EmailSetari> optiuni)
        {
            emailSetari = optiuni.Value;
        }

        public bool TrimiteEmail(DateEmail detalii)
        {
            try
            {
                MimeMessage mesajEmail = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(emailSetari.Nume, emailSetari.EmailId);
                mesajEmail.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(detalii.EmailToNume, detalii.EmailToId);
                mesajEmail.To.Add(emailTo);

                mesajEmail.Subject = detalii.EmailTitlu;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = detalii.EmailContinut;
                mesajEmail.Body = emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect("smtp.mail.yahoo.com", 465, true);
                emailClient.Authenticate(emailSetari.EmailId, emailSetari.Parola);
                emailClient.Send(mesajEmail);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
