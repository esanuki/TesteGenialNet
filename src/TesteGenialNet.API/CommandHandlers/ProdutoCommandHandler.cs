using AutoMapper;
using Azure.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesteGenialNet.Business.Commands.Produtos;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.API.CommandHandlers
{
    public class ProdutoCommandHandler : BaseCommandHandler,
        IRequestHandler<InsertProdutoCommand, bool>,
        IRequestHandler<UpdateProdutoCommand, bool>,
        IRequestHandler<DeleteProdutoCommand, bool>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoFornecedorRepository _produtoFornecedorRepository;
        public ProdutoCommandHandler(
            INotificator notificator,
            IMapper mapper,
            IProdutoRepository produtoRepository,
            IUnitOfWork unitOfWork,
            IProdutoFornecedorRepository produtoFornecedorRepository) : base(notificator, mapper)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _produtoFornecedorRepository = produtoFornecedorRepository;
        }


        public async Task<bool> Handle(InsertProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<Produto>(request);

            var exist = await _produtoRepository.ExistsByExpression(p => p.Descricao == produto.Descricao && produto.Marca == p.Marca);

            if (exist)
            {
                _notificator.NoticationErrors("Esse produto já existe!");
                return false;
            }

            await _unitOfWork.BeginTransaction();

            try
            {               
                await _produtoRepository.Save(produto);

                await _unitOfWork.Commit();

                return true;

            } catch(Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<bool> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!await request.Id.Verify(_produtoRepository, "produto", _notificator))
                return false;

            var produtoOld = await _produtoRepository.GetByExpression(p => p.Id == request.Id);   

            var produto = _mapper.Map<Produto>(request);

            _mapper.Map(produto, produtoOld);

            await _unitOfWork.BeginTransaction();

            try
            {
                await _produtoRepository.Update(produto);

                await _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }

               
        }

        public async Task<bool> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            if (!await request.Id.Verify(_produtoRepository, "produto", _notificator))
                return false;

            if (await _produtoFornecedorRepository.ExistsByExpression(p => p.ProdutoId == request.Id))
            {
                _notificator.NoticationErrors("Existe vinculo desse produto com fornecedor(es), não é possivel efetuar a exclusão");
                return false;
            }

            await _unitOfWork.BeginTransaction();

            try
            {
                await _produtoRepository.Delete(request.Id);

                await _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }


        }
    }
}
