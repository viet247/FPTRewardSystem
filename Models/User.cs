namespace FPTRewardSystem.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRole Role { get; set; }
        // Mối quan hệ 1-n về Ví
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
        // Mối quan hệ 1-1 về Profile cửa hàng(có thể có hoặc không)
        public MerchantProfile MerchantProfile { get; set; }
    }
}
