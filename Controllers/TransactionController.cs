using FluentValidation;
using FPTRewardSystem.API.Dtos;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Models;
using FPTRewardSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace FPTRewardSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/p2p")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IValidator<TransactionRequestDto> _validator;
        public TransactionController(ITransactionService transactionService, IValidator<TransactionRequestDto> validator)
        {
            _transactionService = transactionService;
            _validator = validator;
        }
        //[Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> TransferPoint(TransactionRequestDto requestDto)
        {
            var validationResult = await _validator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.ToDictionary());
            }
            var senderID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _transactionService.TransferPointAsync(senderID, requestDto);
            return Ok(result);
        }
    }
}
