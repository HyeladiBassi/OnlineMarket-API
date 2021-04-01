using System.Threading.Tasks;
using OnlineMarket.Helpers;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class TransactionService : ITransactionService
    {
        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Transaction> deleteTransaction(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedList<Transaction>> GetTransactionListByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Transaction> UpdateTransaction(int id, Transaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}