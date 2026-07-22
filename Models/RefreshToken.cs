namespace FPTRewardSystem.API.Models
{
    public class RefreshToken
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; } = false; // true là đã logout / bị hủy

        // Khóa ngoại liên kết đến User
        public Guid UserId { get; set; }
        public User User { get; set; } = null;
    }
}
