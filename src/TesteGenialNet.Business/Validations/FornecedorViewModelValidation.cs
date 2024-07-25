using FluentValidation;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interop.ViewModels;
using TesteGenialNet.Business.Resources;

namespace TesteGenialNet.Business.Validations
{
    public class FornecedorViewModelValidation : AbstractValidator<FornecedorViewModel>
    {
        public FornecedorViewModelValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Nome"))
                .MaximumLength(200)
                .WithMessage(Resource.MSG_QTD_CARACTERES_200);

            RuleFor(x => x.CNPJ)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "CNPJ"))
                .MaximumLength(14)
                .WithMessage("O campo CNPJ deve ser só numeros e ter no máximo 14 numeros");

            RuleFor(x => x.Telefone)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Telefone"));

            RuleFor(x => x.Endereco)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Invalido, "CEP"));

            RuleFor(x => x.Produtos.Count)
                .GreaterThan(0).WithMessage("Deve ter pelo menos 1 produto fornecido por esse fornecedor!");

            RuleFor(x => x).Custom((x, ctx) =>
            {
                if (!string.IsNullOrWhiteSpace(x.CNPJ) && !x.CNPJ.ValidateCNPJ())
                    ctx.AddFailure("O CNPJ é inválido.");
            });
        }
    }
}
