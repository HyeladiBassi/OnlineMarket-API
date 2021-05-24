using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.API.Extensions;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects;
using OnlineMarket.DataTransferObjects.Product;
using OnlineMarket.DataTransferObjects.Transaction;
using OnlineMarket.Errors;
using OnlineMarket.Helpers;
using OnlineMarket.Models;
using OnlineMarket.Services.Extensions;
using OnlineMarket.Services.Interfaces;

namespace OnlineMarket.API.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public TransactionController(ITransactionService transactionService, IMapper mapper, IProductService productService, IUserService userService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _productService = productService;
            _userService = userService;
        }

        [HttpGet(ApiConstants.TransactionRoutes.GetAllTransactions)]
        public async Task<IActionResult> GetAllTransactions([FromQuery] TransactionResourceParameters parameters)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }

            PagedList<Transaction> pagedTransactions = await _transactionService.GetPagedTransactionList(parameters);
            PagingDto paging = pagedTransactions.ExtractPaging();

            Paginate<TransactionViewDto> result = new Paginate<TransactionViewDto>
            {
                items = _mapper.Map<IEnumerable<TransactionViewDto>>(pagedTransactions),
                pagingInfo = paging
            };
            return Ok(result);
        }

        [HttpGet(ApiConstants.TransactionRoutes.GetTransactionById)]
        public async Task<IActionResult> GetTransactionById([FromParameter("id")] int id)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            Transaction transaction = await _transactionService.GetTransactionById(id);
            TransactionViewDto mappedTransaction = _mapper.Map<TransactionViewDto>(transaction);
            return Ok(mappedTransaction);
        }

        [HttpPost(ApiConstants.TransactionRoutes.CreateTransaction)]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto createDto)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            SystemUser buyer = await _userService.GetUserById(userId);
            Delivery mappedDelivery = _mapper.Map<Delivery>(createDto.Delivery);

            List<OrderCreateDto> orders = createDto.Orders.ToList();
            List<Order> newOrders = new List<Order>();
            for (int i = 0; i < createDto.Orders.Count(); i++)
            {
                
                Product product = await _productService.GetProductById(orders[i].ProductId);
                if (orders[i].Quantity > product.Stock)
                {
                    return BadRequest();
                }
                Order order = new Order
                {
                    Product = product,
                    Quantity = orders[i].Quantity,
                    Price = product.Price
                };
                newOrders.Add(order);
            }
            Transaction transaction = new Transaction
            {
                Currency = createDto.Currency,
                Status = "pending",
                TotalQuantity = newOrders.Sum(x => x.Quantity),
                TotalPrice = newOrders.Sum(x => x.Price),   
                Buyer = buyer,
                Delivery = mappedDelivery
            };
            transaction.Orders = newOrders;
            bool result = await _transactionService.CreateTransaction(userId, transaction);
            
            return Ok(result);
        }
    }
}