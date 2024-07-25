using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TesteGenialNet.Business.Commands.Fornecedores;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.API.CommandHandlers
{
    public class FornecedorCommandHandler : BaseCommandHandler,
        IRequestHandler<InsertProdutoFornecedorCommand, bool>,
        IRequestHandler<UpdateProdutoFornecedorCommand, bool>,
        IRequestHandler<DeleteProdutosFornecedorCommand, bool>,
        IRequestHandler<DeleteFornecedorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        private IProdutoRepository _produtoRepository;
        private IFornecedorRepository _fornecedorRepository;
        private IProdutoFornecedorRepository _produtoFornecedorRepository;

        public FornecedorCommandHandler(
            INotificator notificator,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProdutoRepository produtoRepository,
            IFornecedorRepository fornecedorRepository,
            IProdutoFornecedorRepository produtoFornecedorRepository) : base(notificator, mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _unitOfWork = unitOfWork;
            _produtoFornecedorRepository = produtoFornecedorRepository;
        }

        public async Task<bool> Handle(InsertProdutoFornecedorCommand request, CancellationToken cancellationToken)
        {

            if (await _fornecedorRepository.ExistsByExpression(f => f.CNPJ == request.CNPJ))
            {
                _notificator.NoticationErrors("Já existe fornecedor cadastro para esse CNPJ!");
                return false;
            }

            var listProdutos = _mapper.Map<List<Produto>>(request.Produtos);

            var produtoFornecedores = await PopulateProdutosFornecedorSave(listProdutos);

            var fornecedor = _mapper.Map<Fornecedor>(request);
            fornecedor.Produtos = produtoFornecedores;

            await _unitOfWork.BeginTransaction();

            try
            {
                await _fornecedorRepository.Save(fornecedor);

                await _unitOfWork.Commit();

                return true;

            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }

        public async Task<bool> Handle(UpdateProdutoFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!await request.Id.Verify(_fornecedorRepository, "fornecedor", _notificator))
                return false;

            var listProdutos = _mapper.Map<List<Produto>>(request.Produtos);

            var fornecedor = _mapper.Map<Fornecedor>(request);

            await _unitOfWork.BeginTransaction();

            try
            {
                var produtoFornecedores = await PopulateProdutoFornecedorUpdate(listProdutos, fornecedor);

                var fornecedorOld = await _fornecedorRepository.GetById(fornecedor.Id);
                _mapper.Map(fornecedorOld, fornecedor);
                fornecedor.Produtos = null;

                await _produtoFornecedorRepository.SaveRange(produtoFornecedores);
                await _fornecedorRepository.Update(fornecedor);

                await _unitOfWork.Commit();

                return true;

            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> Handle(DeleteProdutosFornecedorCommand request, CancellationToken cancellationToken)
        {
            if (!await request.Id.Verify(_fornecedorRepository, "fornecedor", _notificator))
                return false;

            foreach (var produto in request.Produtos)
            {
                if (!await _produtoFornecedorRepository.ExistsByExpression(p => p.ProdutoId == produto.Id && p.FornecedorId == request.Id))
                    request.Produtos.Remove(produto);

            }

            await _unitOfWork.BeginTransaction();

            try
            {                
                foreach (var produto in request.Produtos)
                {
                    await _produtoFornecedorRepository.Delete(produto.Id, request.Id);

                    if (!await _produtoFornecedorRepository.ExistsByExpression(p => p.ProdutoId == produto.Id && p.FornecedorId != request.Id))
                        await _produtoRepository.Delete(produto.Id);
                    
                }

                await _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }


        }

        public async Task<bool> Handle(DeleteFornecedorCommand request, CancellationToken cancellationToken)
        {

            if (!await request.Id.Verify(_fornecedorRepository, "fornecedor", _notificator))
                return false;

            var fornecedor = await _fornecedorRepository.GetFornecedorWithProdutosByExpression(f => f.Id == request.Id);

            await _unitOfWork.BeginTransaction();

            try
            {               
                foreach (var produto in fornecedor.Produtos)
                    await _produtoFornecedorRepository.Delete(produto.ProdutoId, fornecedor.Id);

                await _fornecedorRepository.Delete(fornecedor.Id);

                await _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }            
        }

        #region métodos privados

        private async Task<List<ProdutoFornecedor>> PopulateProdutosFornecedorSave(List<Produto> listProdutos)
        {
            List<ProdutoFornecedor> produtoFornecedores = new();

            foreach (var produto in listProdutos)
            {
                var produtoBanco = await _produtoRepository.GetByExpression(p => p.Descricao == produto.Descricao && produto.Marca == p.Marca);
                if (produtoBanco == null)
                {
                    await _produtoRepository.Save(produto);
                    produtoFornecedores.Add(new ProdutoFornecedor { ProdutoId = produto.Id, ValorCompra = produto.ValorCompra });
                }
                else
                {
                    produtoFornecedores.Add(new ProdutoFornecedor { ProdutoId = produtoBanco.Id, ValorCompra = produto.ValorCompra });
                }
            }

            return produtoFornecedores;
        }

        private async Task<List<ProdutoFornecedor>> PopulateProdutoFornecedorUpdate(List<Produto> listProdutos, Fornecedor fornecedor)
        {
            List<ProdutoFornecedor> produtoFornecedores = new();

            foreach (var produto in listProdutos)
            {
                if (produto.Id == 0)
                {
                    var produtoBanco = await _produtoRepository.GetByExpression(p => p.Descricao == produto.Descricao && p.Marca == produto.Marca);
                    if (produtoBanco == null)
                    {
                        await _produtoRepository.Save(produto);
                        produtoFornecedores.Add(new ProdutoFornecedor { ProdutoId = produto.Id, FornecedorId = fornecedor.Id, ValorCompra = produto.ValorCompra });
                    }
                    else
                    {
                        await _produtoFornecedorRepository.Delete(produtoBanco.Id, fornecedor.Id);
                        produtoFornecedores.Add(new ProdutoFornecedor { ProdutoId = produtoBanco.Id, FornecedorId = fornecedor.Id, ValorCompra = produto.ValorCompra });
                    }

                }
                else
                {
                    var produtoOld = await _produtoRepository.GetById(produto.Id);
                    _mapper.Map(produtoOld, produto);

                    await _produtoRepository.Update(produto);
                    await _produtoFornecedorRepository.Delete(produto.Id, fornecedor.Id);

                    produtoFornecedores.Add(new ProdutoFornecedor { ProdutoId = produtoOld.Id, FornecedorId = fornecedor.Id, ValorCompra = produto.ValorCompra });
                }
            }

            return produtoFornecedores;
        }
        #endregion
    }
}
