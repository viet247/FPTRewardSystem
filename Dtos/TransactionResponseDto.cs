namespace FPTRewardSystem.API.Dtos
{
    public class TransactionResponseDto
    {
        public Guid ID { get; set; }
        public Guid ToWalletID { get; set; }
        public decimal ExistingBalance { get; set; }
        public string? Description { get; set; }
    }
}
