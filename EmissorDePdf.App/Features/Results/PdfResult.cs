using EmissorDePdf.Domain.Models;
using EmissorDePdf.Shared.ICommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.App.Features.Results
{
    public class PdfResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public PdfModel Pdf { get; set; }
    }
}
