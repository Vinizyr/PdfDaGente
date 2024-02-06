using EmissorDePdf.App.Features.Commands;
using EmissorDePdf.App.Features.Results;
using EmissorDePdf.Domain.Models;
using EmissorDePdf.Shared.ICommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.App.Features.Handlers
{
    //public class PdfHandler : ICommandHandler<GerarPdfCommand>
    //{
    //    public async Task<ICommandResult> Handle(GerarPdfCommand command)
    //    {
    //        var pdfModel = new PdfModel()
    //        {
    //            Cpf = command.Cpf,
    //            DataEmissao = command.DataEmissao,
    //            Email = command.Email,
    //            Nome = command.Nome,
    //            OrgaoEmissor = command.OrgaoEmissor,
    //            Produtos = command.ProdutosCommand.Select(p => new ProdutosViewModel
    //            { Nome = p.Nome, Preco = p.Preco }).ToList(),
    //            ValorTotal = command.ValorTotal
    //        };

    //        return new PdfResult()
    //        {
    //            Sucesso = true,
    //            Mensagem = "",
    //            Pdf = pdfModel
    //        };
    //    }
    //}
}
