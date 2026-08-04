namespace FPTRewardSystem.API.Dtos
{
    public class IssuePointsResponseDto
    {
        public DateTime ExecutedAt { get; set; }
        public DateTime TargetMonth { get; set; }
        public Decimal IssueAmountPerUser { get; set; }
        public int ToTalUsersProcessed { get; set; }
        public Decimal TotalPointsIssued { get; set; }
        public string? Message { get; set; }
    }
}
