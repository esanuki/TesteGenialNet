using FluentValidation;
using TesteGenialNet.Business.Interop.ViewModels;
using TesteGenialNet.Business.Resources;

namespace TesteGenialNet.Business.Validations
{
    public class ProdutoViewModelValidation : AbstractValidator<ProdutoViewModel>
    {
        public ProdutoViewModelValidation()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Descriçao"))
                .NotNull()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Descrição"))
                .MaximumLength(200)
                .WithMessage(Resource.MSG_QTD_CARACTERES_200);

            RuleFor(x => x.Marca)
                .NotEmpty()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Marca"))
                .NotNull()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Marca"));

            RuleFor(x => x.UnidadeMedida)
                .IsInEnum()
                .WithMessage(string.Format(Resource.MSG_Campo_Obrigatorio, "Unidade de medida"));
        }
    }
}
