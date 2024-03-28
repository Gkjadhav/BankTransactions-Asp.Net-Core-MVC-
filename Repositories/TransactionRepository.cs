using Microsoft.EntityFrameworkCore;
using BankTransaction.Interfaces;
using BankTransaction.Models;
using BankTransaction.Pagination;
using BankTransactions.Models;

namespace BankTransaction.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            this._context = context;
        }

        public Transaction? Get(int Id)
        {
            return _context.Transactions.AsNoTracking().SingleOrDefault(x => x.TransactionId == Id);
        }
        
        public async Task<Transaction?> GetAsync(int Id)
        {
            return await _context.Transactions.AsNoTracking().SingleOrDefaultAsync(x => x.TransactionId == Id);
        }

        public PaginatedList<Transaction> GetAll(QueryParameters queryParameters)
        {
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchText))
            {
                PaginatedList<Transaction> products = _context.Transactions.AsNoTracking().Where(x => x.BeneficiaryName.Contains(queryParameters.SearchText)).Pagination(queryParameters.PageIndex, queryParameters.PageSize);
                products.SearchText = queryParameters.SearchText;
                return products;
            }
            else return _context.Transactions.AsNoTracking().Pagination(queryParameters.PageIndex, queryParameters.PageSize);
        }
        
        public async Task<PaginatedList<Transaction>> GetAllAsync(QueryParameters queryParameters)
        {
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchText))
            {
                PaginatedList<Transaction> products = await _context.Transactions.AsNoTracking().Where(x => x.BeneficiaryName.Contains(queryParameters.SearchText)).PaginationAsync(queryParameters.PageIndex, queryParameters.PageSize);
                products.SearchText = queryParameters.SearchText;
                return products;
            }
            else return await _context.Transactions.PaginationAsync(queryParameters.PageIndex, queryParameters.PageSize);
        }
    }
}