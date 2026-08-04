namespace FPTRewardSystem.API.Dtos
{
    public class IssuePointsRequestDto
    {
        public decimal IssueAmountPerUser { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
