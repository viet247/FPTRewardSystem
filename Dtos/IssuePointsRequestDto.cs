namespace FPTRewardSystem.API.Dtos
{
    public class IssuePointsRequestDto
    {
        public decimal IssueAmountPerUser { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
