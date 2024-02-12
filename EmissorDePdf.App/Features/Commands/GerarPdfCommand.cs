using EmissorDePdf.Shared.ICommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.App.Features.Commands
{
    public class GerarPdfCommand : ICommand
    {
        //Dados do Cliente
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        //Produtos
        public List<Produtos> ProdutosCommand { get; set; }

        //Dados da nota fiscal
        public DateTime DataEmissao { get; set; } = DateTime.Now;
        public float ValorTotal { get; set; }
        public string OrgaoEmissor { get; set; }
    }

    public class Produtos
    {
        public string Nome { get; set; }
        public float Preco { get; set; }
    }
}
