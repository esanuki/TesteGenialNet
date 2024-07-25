using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.Data.Repositorys
{
    public class ProdutoFonecedorRepository : IProdutoFornecedorRepository
    {
        private readonly TesteGenialNetContext _context;
        private readonly DbSet<ProdutoFornecedor> _dbSet;

        public ProdutoFonecedorRepository(TesteGenialNetContext context)
        {
            _context = context;
            _dbSet = context.Set<ProdutoFornecedor>();
        }

        public async Task SaveRange(List<ProdutoFornecedor> produtos)
        {
            await _dbSet.AddRangeAsync(produtos);
            await _context.SaveChangesAsync();  
        }

        public async Task Delete(int produtoId, int fornecedorId)
        {
            _dbSet.Remove(new ProdutoFornecedor { ProdutoId = produtoId, FornecedorId = fornecedorId });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByExpression(Expression<Func<ProdutoFornecedor, bool>> predicate)
            => await _dbSet.AsNoTracking().Where(predicate).AnyAsync();
    }
}
