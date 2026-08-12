namespace FPTRewardSystem.API.Dtos
{
    public class PaymentWithOtpRequestDto
    {
        public string UserId { get; set; } = string.Empty;
        public string MerchantId { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
