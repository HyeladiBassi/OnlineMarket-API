using System.Threading.Tasks;
using OnlineMarket.Models;
using OnlineMarket.Helpers;

namespace OnlineMarket.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Transaction> GetTransactionById(int id);
        public Task<PagedList<Transaction>> GetTransactionListByUserId(string userId);
        public Task<Transaction> CreateTransaction(Transaction transaction);
        public Task<Transaction> UpdateTransaction(int id, Transaction transaction);
        public Task<Transaction> deleteTransaction(int id);
    }
}