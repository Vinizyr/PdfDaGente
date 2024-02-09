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

        public async Task SendEmailAsync(MailRequest maillrequest)
        {

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            {
                connection.Open();
                var query = "SELECT TOP 5 DadosPdf FROM PdfDocumentos";
                var pdfDataList = connection.Query<byte[]>(query).ToList();

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(maillrequest.ToEmail));
                email.Subject = maillrequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = maillrequest.Body;

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
            //if (sslPolicyErrors == SslPolicyErrors.None)
            //    return true;

            //// if there are errors in the certificate chain, look at each error to determine the cause.
            //if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            //{
            //    if (chain != null && chain.ChainStatus != null)
            //    {
            //        foreach (var status in chain.ChainStatus)
            //        {
            //            if ((certificate.Subject.Contains(".tjse.jus.br") && (status.Status == X509ChainStatusFlags.UntrustedRoot)))
            //            {
            //                // self-signed certificates with an untrusted root are valid.
            //                continue;
            //            }
            //            else if (status.Status != X509ChainStatusFlags.NoError)
            //            {
            //                // if there are any other errors in the certificate chain, the certificate is invalid,
            //                // so the method returns false.
            //                return false;
            //            }
            //        }
            //    }

            //    // When processing reaches this line, the only errors in the certificate chain are
            //    // untrusted root errors for self-signed certificates. These certificates are valid
            //    // for default Exchange server installations, so return true.
            //    return true;
            //}

            //return false;
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