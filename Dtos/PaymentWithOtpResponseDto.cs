namespace FPTRewardSystem.API.Dtos
{
    public class PaymentWithOtpResponseDto
    {
        public string TransactionId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime PaymentTime { get; set; }
    }
}
