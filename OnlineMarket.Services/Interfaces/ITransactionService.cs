using System.Threading.Tasks;
using OnlineMarket.Models;
using OnlineMarket.Helpers;
using OnlineMarket.DataTransferObjects.Transaction;
using System.Collections.Generic;

namespace OnlineMarket.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<PagedList<Transaction>> GetPagedTransactionsAsync();
        Task<ICollection<Transaction>> GetTransactionListByUserId(string userId);
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<Transaction> UpdateTransaction(int id, TransactionUpdateDto transaction);
        Task<Transaction> DeleteTransaction(int id);
    }
}