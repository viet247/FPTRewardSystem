namespace FPTRewardSystem.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

        // Người gửi (Cho phép Null nếu là Admin cấp phát)
        public Guid? SenderWalletId { get; set; }
        public Wallet? SenderWallet { get; set; }

        // Người nhận
        public Guid ReceiverWalletId { get; set; }
        public Wallet ReceiverWallet { get; set; }
    }
}
