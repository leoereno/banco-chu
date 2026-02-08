namespace BancoChu.Models
{
    public class OperationResult
    {
        public bool Success {  get; set; }
        public string Message { get; set; }
        public static OperationResult Fail(string msg) => new OperationResult { Success = false, Message = msg };
        public static OperationResult Ok() => new OperationResult { Success = true };
    }
}
