using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineMarket.DataAccess;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.Transaction;
using OnlineMarket.Errors;
using OnlineMarket.Helpers;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
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

        // public async Task<bool> CheckProducts(ICollection<OrderCreateDto> orders)
        // {
        //     ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
        //     foreach (var item in orders)
        //     {
        //         var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
        //         if (product != null)
        //         {
        //             var error = errorBuilder.AddField(, "Product with id("") does not exist");
        //         }
        //     }
        // }

        public async Task<bool> CreateTransaction(string userId, Transaction transaction)
        {
            foreach (var item in transaction.Orders)
            {
                await DecreaseStock(item.Product.Id, item.Quantity);
            }
            List<EntityEntry> changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
            await _context.Transactions.AddAsync(transaction);
            return await Save();
        }

        public async Task<bool> DecreaseStock(int productId, int quantity)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            product.Stock = product.Stock - quantity;
            _context.Products.Update(product);
            return await Save();
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            Transaction item = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            _context.Transactions.Remove(item);
            return await Save();
        }

        public async Task<PagedList<Transaction>> GetPagedTransactionList(TransactionResourceParameters parameters)
        {
            PagedList<Transaction> transactions = await _context.Transactions
                .Include(x => x.Orders)
                .WhereLtEq(x => x.TotalPrice, parameters.AmountLt)
                .WhereGtEq(x => x.TotalPrice, parameters.AmountGt)
                .ToPagedListAsync(parameters.pageNumber, parameters.pageSize);

            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            List<Order> li = new List<Order>();
            Transaction transaction = await _context.Transactions
                .Include(x => x.Buyer)
                .Include(x => x.Orders)
                .Include(x => x.Delivery)
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);

            foreach (var order in transaction.Orders)
            {
                li.Add(await GetOrder(order.Id));
            }
            return transaction;
        }

        public async Task<PagedList<Transaction>> GetTransactionListByUserId(string userId, TransactionResourceParameters parameters)
        {
            PagedList<Transaction> transactions = await _context.Transactions
                .Include(x => x.Orders)
                .Where(x => x.Buyer.Id == userId)
                .WhereLtEq(x => x.TotalPrice, parameters.AmountLt)
                .WhereGtEq(x => x.TotalPrice, parameters.AmountGt)
                .ToPagedListAsync(parameters.pageNumber, parameters.pageSize);

            return transactions;
        }

        private async Task<Order> GetOrder(int orderId)
        {
            Order order = await _context.Orders.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == orderId);
            return order;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}