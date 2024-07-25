using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interop.ViewModels;

namespace TesteGenialNet.Business.Helpers
{
    public static class ValidationHelpers
    {
        public static async Task<IList<ProdutoViewModel>> ProdutosValidation(this IList<ProdutoViewModel> produtos, INotificator notificator)
        {
            foreach (var produto in produtos)
            {
                await produto.ProdutoValidation(notificator);
                await produto.Validate();
                if (!produto.IsValid)
                    notificator.NotificationValidationResult(produto.ValidationResult);
            }

            return produtos;
        }

        public static async Task<ProdutoViewModel> ProdutoValidation(this ProdutoViewModel produto, INotificator notificator)
        {
            await produto.Validate();
            if (!produto.IsValid)
                notificator.NotificationValidationResult(produto.ValidationResult);

            return produto;
        }

        public static async Task<FornecedorViewModel> FornecedorValidation(this FornecedorViewModel fornecedor, INotificator notificator)
        {
            await fornecedor.Validate();
            if (!fornecedor.IsValid)
                notificator.NotificationValidationResult(fornecedor.ValidationResult);

            return fornecedor;
        }

        public static async Task<bool> Verify<T>(this int id, IRepository<T> repository, string descricao, INotificator notificator) where T : BaseEntity
        {
            if (id == 0)
            {
                notificator.NoticationErrors("#descricao inválido!".Replace("#descricao", descricao));
                return false;
            }

            if (!await repository.ExistsByExpression(p => p.Id == id))
            {
                notificator.NoticationErrors("#descricao não encontrado!".Replace("#descricao", descricao));
                return false;
            }

            return true;
        }
    }
}
