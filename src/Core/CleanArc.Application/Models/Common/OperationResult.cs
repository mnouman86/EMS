namespace CleanArc.Application.Models.Common;

public class OperationResult<TResult>
{
    public TResult Result { get; private set; }

    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public bool IsException { get; set; }
    public bool IsNotFound { get; private set; }
    public static OperationResult<TResult> SuccessResult(TResult result, int statusCode=200, string message="Success")
    {
        // return new OperationResult<TResult>{Result = result,IsSuccess = true, StatusCode=statusCode,Message=message};
        if (result != null &&
         result.GetType().GetProperty("Code")?.GetValue(result) != null &&
         result.GetType().GetProperty("Message")?.GetValue(result) != null &&
         result.GetType().GetProperty("IsSuccess")?.GetValue(result) != null)
        {
            var code = (int)result.GetType().GetProperty("Code").GetValue(result);
            var msg = result.GetType().GetProperty("Message").GetValue(result)?.ToString();
            var isSuccess =(bool) result.GetType().GetProperty("IsSuccess").GetValue(result);

            return new OperationResult<TResult>
            {
                Result = result,
                IsSuccess = isSuccess,
                StatusCode = code,
                Message = msg
            };
        }

        // Default behavior if result doesn't have Code and Message properties
        return new OperationResult<TResult>
        {
            Result = result,
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }

    //public static OperationResult<TResult> FailureResult(string message,TResult result=default, int statusCode=400)
    //{
    //    return new OperationResult<TResult>{Result = result,ErrorMessage = message,IsSuccess = false,StatusCode=statusCode};
    //}
    public static OperationResult<TResult> FailureResult(string message, int statusCode = 400)
    {
        return new OperationResult<TResult> { ErrorMessage = message, IsSuccess = false, StatusCode = statusCode };
    }

    public static OperationResult<TResult> NotFoundResult(string message)
    {
        return new OperationResult<TResult> { ErrorMessage = message, IsSuccess = false, IsNotFound = true };
    }
}