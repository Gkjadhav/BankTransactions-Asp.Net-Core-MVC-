using BankTransaction.Models;
using BankTransaction.Pagination;

namespace BankTransaction.Interfaces
{
    public interface ITransactionRepository
    {
        PaginatedList<Transaction> GetAll(QueryParameters queryParameters);
        Task<PaginatedList<Transaction>> GetAllAsync(QueryParameters queryParameters);
        Transaction? Get(int Id);
        Task<Transaction?> GetAsync(int Id);
    }
}