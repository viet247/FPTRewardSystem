namespace FPTRewardSystem.API.Dtos
{
    public class TransactionOtpResponseDto
    {
        public string OtpCode { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
