namespace FPTRewardSystem.API.Models
{
    public class MerchantProfile
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        // Mối quan hệ 1-n về Ví
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
