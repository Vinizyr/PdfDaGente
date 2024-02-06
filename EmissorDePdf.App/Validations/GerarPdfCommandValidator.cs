using EmissorDePdf.App.Features.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.App.Validations
{
    public class GerarPdfCommandValidator : AbstractValidator<GerarPdfCommand>
    {
        public GerarPdfCommandValidator()
        {
            RuleFor(command => command.Nome).NotEmpty().WithMessage("O campo Nome é obrigatório.");
            RuleFor(command => command.Cpf).NotEmpty().WithMessage("O campo CPF é obrigatório.");
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("O campo Email é obrigatório e deve ser um endereço de e-mail válido.");

            RuleFor(command => command.ProdutosCommand).NotNull().WithMessage("A lista de produtos não pode ser nula.");
            RuleForEach(command => command.ProdutosCommand)
                .SetValidator(new ProdutoValidator());

            RuleFor(command => command.DataEmissao).NotEmpty().WithMessage("O campo Data de Emissão é obrigatório.");
            RuleFor(command => command.ValorTotal).GreaterThan(0).WithMessage("O Valor Total deve ser maior que zero.");
            RuleFor(command => command.OrgaoEmissor).NotEmpty().WithMessage("O campo Órgão Emissor é obrigatório.");
        }
    }

    public class ProdutoValidator : AbstractValidator<Produtos>
    {
        public ProdutoValidator()
        {
            RuleFor(produto => produto.Nome).NotEmpty().WithMessage("O campo Nome do Produto é obrigatório.");
            RuleFor(produto => produto.Preco).GreaterThan(0).WithMessage("O Preço do Produto deve ser maior que zero.");
        }
    }
}
