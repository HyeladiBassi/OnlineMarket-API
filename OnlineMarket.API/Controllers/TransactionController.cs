using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Contracts;
using OnlineMarket.DataTransferObjects.Transaction;
using OnlineMarket.Errors;
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

        public TransactionController(ITransactionService transactionService, IMapper mapper, IProductService productService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet(ApiConstants.TransactionRoutes.GetAllTransactions)]
        public async Task<IActionResult> GetAllTransactions()
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            return Ok();
        }

        // [HttpGet]
        // public async Task<IActionResult> GetTransactionsByUserId()
        // {
        //     string userId = HttpContext.GetUserIdFromToken();
        //     if (string.IsNullOrWhiteSpace(userId))
        //     {
        //         return Forbid();
        //     }
        //     return Ok();
        // }

        [HttpGet(ApiConstants.TransactionRoutes.GetTransactionById)]
        public async Task<IActionResult> GetTransactionById([FromParameter("id")]int id)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            return Ok();
        }

        [HttpPost(ApiConstants.TransactionRoutes.CreateTransaction)]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto createDto)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            return Ok();
        }

        [HttpPut(ApiConstants.TransactionRoutes.UpdateTransaction)]
        public async Task<IActionResult> UpdateTransaction(int transactionId, TransactionUpdateDto updateDto)
        {
            string userId = HttpContext.GetUserIdFromToken();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Forbid();
            }
            return Ok();
        }
    }
}