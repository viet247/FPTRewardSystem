using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> TransferPointAsync(Guid senderID, TransactionRequestDto requestDto);
        Task<PagedResult<TransactionHistoryResponseDto>> GetTransactionsAsync(Guid userId, int pageNumber, int pageSize);
    }
}
