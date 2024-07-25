using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TesteGenialNet.Business.Dtos;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.Data.Repositorys
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(TesteGenialNetContext context) : base(context)
        {
        }

        public async Task<FornecedorDto> GetFornecedorDto(Expression<Func<FornecedorDto, bool>> predicate)
        {
            return await _dbSet
                .Include(f => f.Produtos)
                .ThenInclude(p => p.Produto)
                .Select(x => new FornecedorDto
                {
                    Id = x.Id,
                    CNPJ = x.CNPJ,
                    Endereco = x.Endereco,
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    Produtos = x.Produtos.Select(v => new ProdutoDto
                    {
                        Id = v.Produto.Id,
                        Descricao = v.Produto.Descricao,
                        Marca = v.Produto.Marca,
                        UnidadeMedida = v.Produto.UnidadeMedida.GetDescription(),
                        ValorCompra = v.ValorCompra
                    }).ToList()
                })
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IList<FornecedorDto>> GetAllFornecedorDto()
        {
            return await _dbSet
                .Include(f => f.Produtos)
                .ThenInclude(p => p.Produto)
                .Select(x => new FornecedorDto
                {
                    Id = x.Id,
                    CNPJ = x.CNPJ,
                    Endereco = x.Endereco,
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    Produtos = x.Produtos.Select(v => new ProdutoDto
                    {
                        Id = v.Produto.Id,
                        Descricao = v.Produto.Descricao,
                        Marca = v.Produto.Marca,
                        UnidadeMedida = v.Produto.UnidadeMedida.GetDescription(),
                        ValorCompra = v.Produto.ValorCompra
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Fornecedor> GetFornecedorWithProdutosByExpression(Expression<Func<Fornecedor, bool>> predicate)
        {
            return await _dbSet
                .Include(f => f.Produtos)
                .ThenInclude(p => p.Produto)
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

    }
}
