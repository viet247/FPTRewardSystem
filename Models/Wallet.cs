
namespace FPTRewardSystem.API.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public WalletType Type { get; set; }
        public Guid UserId { get; set; } // Liên kết với User
        public User User { get; set; }

        // Một ví có thể có nhiều lịch sử giao dịch (với vai trò người gửi hoặc nhận)
        public ICollection<Transaction> IncomingTransactions { get; set; }
        public ICollection<Transaction> OutgoingTransactions { get; set; }
    }
}
