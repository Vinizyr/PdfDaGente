using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Domain.Models
{
    public class PdfModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        //Produtos
        public List<ProdutosViewModel> Produtos { get; set; }

        //Dados da nota fiscal
        public DateTime DataEmissao { get; set; } = DateTime.Now;
        public float ValorTotal { get; set; }
        public string OrgaoEmissor { get; set; }
    }
    public class ProdutosViewModel
    {
        public string Nome { get; set; }
        public float Preco { get; set; }
    }
}
