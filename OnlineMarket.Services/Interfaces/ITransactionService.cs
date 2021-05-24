using System.Threading.Tasks;
using OnlineMarket.Models;
using OnlineMarket.Helpers;
using OnlineMarket.DataTransferObjects.Transaction;
using System.Collections.Generic;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.Errors;

namespace OnlineMarket.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<PagedList<Transaction>> GetPagedTransactionList(TransactionResourceParameters parameters);
        Task<PagedList<Transaction>> GetTransactionListByUserId(string userId, TransactionResourceParameters parameters);
        Task<Transaction> GetTransactionById(int id);
        Task<bool> CreateTransaction(string userId, Transaction transaction);
        Task<bool> DeleteTransaction(int id);
        Task<bool> DecreaseStock(int productId, int quantity);
        // Task<ResponseWrapper<ICollection<OrderCreateDto>, ErrorTypes>> CheckProducts(ICollection<OrderCreateDto> orders);
    }
}