using EmissorDePdf.API.Views.ModelViews;
using EmissorDePdf.App.Features.Commands;
using EmissorDePdf.Domain.IRepository;
using EmissorDePdf.Email;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace EmissorDePdf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfRepository _pdfRepository;
        private readonly IEmailSender _emailSender;

        public PdfController(IPdfRepository pdfRepository, IEmailSender emailSender)
        {
            _pdfRepository = pdfRepository;
            _emailSender = emailSender;
        }

        [HttpPost("")]
        public async Task<IActionResult> Fatura([FromBody] GerarPdfCommand command)
        {
            var pdfModel = new PdfViewModel()
            {
                Cpf = command.Cpf,
                DataEmissao = command.DataEmissao,
                Email = command.Email,
                Nome = command.Nome,
                OrgaoEmissor = command.OrgaoEmissor,
                Produtos = command.ProdutosCommand.Select(p => new ProdutosViewModel
                { Nome = p.Nome, Preco = p.Preco }).ToList(),
                ValorTotal = command.ValorTotal
            };

            ViewAsPdf view = new("Views\\Pdf\\PdfView.cshtml", pdfModel);
            byte[] dados = await view.BuildFile(ControllerContext);
            var pdf = new FileContentResult(dados, "application/pdf");
            await _pdfRepository.SavePdf(pdf);
            return new FileContentResult(dados, "application/pdf")
            {
                FileDownloadName = "NotaFiscal.pdf"
            };
        }

        [HttpPost("email")]
        public async Task<IActionResult> EnviarEmail()
        {
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "marcozyr12@gmail.com";
                mailRequest.Subject = "Teste Pdf";
                mailRequest.Body = "PDF";
                await _emailSender.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception) 
            {
                throw;
            }
        }



    }
}
