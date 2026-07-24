using FPTRewardSystem.API.Models;

namespace FPTRewardSystem.API.Dtos
{
    public class TransactionHistoryResponseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        // Xác định loại giao dịch đối với User hiện tại
        public string TransactionType { get; set; } // IN là nhận, Out là gửi
    }
}
