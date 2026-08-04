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
    [Route("api/v1/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IValidator<TransactionRequestDto> _validator;
        private readonly IValidator<TransactionHistoryRequestDto> _transHisReqValidator;
        public TransactionController(ITransactionService transactionService, IValidator<TransactionRequestDto> validator, IValidator<TransactionHistoryRequestDto> transHisReqValidator)
        {
            _transactionService = transactionService;
            _validator = validator;
            _transHisReqValidator = transHisReqValidator;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("p2p")]
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionHistoryRequestDto requestDto)
        {
            var validationResult = await _transHisReqValidator.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                throw new AppValidationException(validationResult.ToDictionary());
            }

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(currentUserId, out var userId))
            {
                return Unauthorized();
            }
            var result = await _transactionService.GetTransactionsAsync(userId, requestDto.PageNumber, requestDto.PageSize);
            return Ok(result);
        }
        [HttpPost("issue-points")]
        public async Task<IActionResult> IssuePoints(IssuePointsRequestDto requestDto)
        {
            
        }

    }
}
