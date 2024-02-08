using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest maillrequest);
    }
}
