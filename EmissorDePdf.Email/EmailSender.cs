using Microsoft.Extensions.Options;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace EmissorDePdf.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> options, IConfiguration configuration)
        {
            _emailSettings = options.Value;
            _configuration = configuration;
        }

        public async Task SendEmailAsync()
        {

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            {
                connection.Open();
                var query = "SELECT TOP 5 DadosPdf FROM PdfDocumentos";
                var pdfDataList = connection.Query<byte[]>(query).ToList();

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.To.Add(MailboxAddress.Parse("marcozyr12@gmail.com"));
                email.Subject = "Pdfs de compras";
                var builder = new BodyBuilder();
                builder.HtmlBody = "PDF";

                // Adiciona anexos ao e-mail
                foreach (var pdfData in pdfDataList)
                {
                    builder.Attachments.Add("Fatura.pdf", pdfData, ContentType.Parse("application/pdf"));
                }

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = ValidaCertificadoValidation;
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            
        }
        private bool ValidaCertificadoValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
       