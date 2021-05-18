using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.Transaction;
using OnlineMarket.Helpers;
using OnlineMarket.Models;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.Services.Main
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly UserManager<SystemUser> _userManager;

        public TransactionService(UserManager<SystemUser> userManager, DataContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        // public async Task<Transaction> CreateTransaction(TransactionCreateDto transactionDto,ICollection<Product> products, string userId)
        // {
            // SystemUser user = await _userManager.FindByIdAsync(userId);
            // Transaction transaction = new Transaction
            // {
            //     Buyer = user,
            //     Products = products,
            //     Status = "Pending",
            //     Delivery = transactionDto.Delivery,
                
            // };
        //     return new Transaction();
        // }

        public Task<Transaction> DeleteTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Transaction>> GetPagedTransactionList(TransactionResourceParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Transaction>> GetTransactionListByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> UpdateTransaction(int id, TransactionUpdateDto transaction)
        {
            throw new NotImplementedException();
        }
    }
}