namespace FPTRewardSystem.API.Dtos
{
    public class TransactionRequestDto
    {
        public Guid ToWalletID { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
