namespace FPTRewardSystem.API.Exceptions
{
    public class AppValidationException : Exception
    {
        // Bảng băm lưu trữ: Key là tên thuộc tính, Value là danh sách lỗi
        public IDictionary<string, string[]> Errors { get; }
        public AppValidationException(IDictionary<string, string[]> errors) : base("Một hoặc nhiều lỗi đã xảy ra")
        {
            this.Errors = errors;
        }
    }
}
