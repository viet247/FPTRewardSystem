namespace FPTRewardSystem.API.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedResult(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            this.Items = items;
            this.TotalCount = totalCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
