

namespace EmissorDePdf.API.Views.ModelViews
{
    public class PdfViewModel
    {
        //Dados do Cliente
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
