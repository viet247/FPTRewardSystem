using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FPTRewardSystem.API.Dtos
{
    public class TransactionHistoryRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
