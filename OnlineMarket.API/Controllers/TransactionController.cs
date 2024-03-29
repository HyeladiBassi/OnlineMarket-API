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
            return Ok(pagedTransactions);
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

        [HttpPut(ApiConstants.TransactionRoutes.UpdateTransaction)]
        public async Task<IActionResult> UpdateTransaction([FromParameter("id")] int id,TransactionUpdateDto updateDto)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            bool result = await _transactionService.UpdateTransactionStatus(id, updateDto.Status);
            if (result)
            {
                return Ok("Status updated");
            }
            return BadRequest("Something went wrong!");
        }

        [HttpPost(ApiConstants.TransactionRoutes.CreateTransaction)]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto createDto)
        {
            ErrorBuilder<ErrorTypes> errorBuilder = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody);
            
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            if (createDto.Orders.Count() <= 0)
            {
                return NotFound(errorBuilder
                    .ChangeType(ErrorTypes.InvalidRequestBody)
                    .SetMessage("Order must have at least one product!")
                    .Build());
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
                    return BadRequest("Not enough stock for product " + product.Name);
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
            var result = await _transactionService.CreateTransaction(userId, transaction);
            TransactionViewDto mappedTransaction = _mapper.Map<TransactionViewDto>(result);
            return Ok(mappedTransaction);
        }
    }
}