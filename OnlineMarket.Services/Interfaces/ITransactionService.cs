using System.Threading.Tasks;
using OnlineMarket.Models;
using OnlineMarket.Helpers;
using OnlineMarket.DataTransferObjects.Transaction;
using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Product;

namespace OnlineMarket.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<PagedList<Transaction>> GetPagedTransactionList(TransactionResourceParameters parameters);
        Task<ICollection<Transaction>> GetTransactionListByUserId(string userId);
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(TransactionCreateDto transaction,ICollection<Product> products, string userId);
        Task<Transaction> UpdateTransaction(int id, TransactionUpdateDto transaction);
        Task<Transaction> DeleteTransaction(int id);
    }
}