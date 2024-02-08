using Microsoft.Extensions.Options;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace EmissorDePdf.Email
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest maillrequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(maillrequest.ToEmail));   
            email.Subject = maillrequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = maillrequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }



    }
}
        //using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //public List<byte[]> ObterPdfsDoBancoDeDados()
        //{
        //    using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    {
        //        connection.Open();
        //        var query = "SELECT TOP 5 PdfData FROM PdfDocumentos";
        //        //return connection.Query<byte[]>(query).ToList();
        //    }
        //}

                    //connection.Open();
                    //var query = "SELECT TOP 5 DadosPdf FROM PdfDocumentos";
                    //var pdfDataList = connection.Query<byte[]>(query).ToList();