using CleanArc.Domain.Common;

namespace CleanArc.Application.Models.Common;

public class OperationResult<TResult>
{
    public TResult Result { get; private set; }

    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public int StatusCode { get; set; }
    public string ErrorCode { get; set; } // New field
    public string SuccessCode { get; set; } // New field

    public int TotalCount { get; set; }
    public string Message { get; set; }
    public bool IsException { get; set; }
    public bool IsNotFound { get; private set; }
    public static OperationResult<TResult> SuccessResult(TResult result, int statusCode=200, string message="Success", int totalCount=0, string successCode = null)
    {
        // return new OperationResult<TResult>{Result = result,IsSuccess = true, StatusCode=statusCode,Message=message};
       
        if (result != null &&
         result.GetType().GetProperty("Code")?.GetValue(result) != null &&
         result.GetType().GetProperty("Message")?.GetValue(result) != null &&
         result.GetType().GetProperty("IsSuccess")?.GetValue(result) != null)
        {
            statusCode = (int)result.GetType().GetProperty("Code").GetValue(result);
            message = result.GetType().GetProperty("Message").GetValue(result)?.ToString();
            successCode = result?.GetType()?.GetProperty("SuccessCode")?.GetValue(result)?.ToString();
            if (successCode!=null)
            {
                message= SuccessMessages.GetMessage(successCode);
            }
            var isSuccess =(bool) result.GetType().GetProperty("IsSuccess").GetValue(result);

            var propertyInfo = result.GetType().GetProperty("TotalCount");
            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(result);
                if (value != null && int.TryParse(value.ToString(), out int parsedValue))
                {
                    totalCount = parsedValue;
                }
            }
            //totalCount =(int) result.GetType().GetProperty("TotalCount").GetValue(result);
            
            return new OperationResult<TResult>
            {
                Result = result,
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                TotalCount=totalCount
            };
        }
        if (successCode != null)
        {
            message = SuccessMessages.GetMessage(successCode);
        }
        // Default behavior if result doesn't have Code and Message properties
        return new OperationResult<TResult>
        {
            Result = result,
            IsSuccess = statusCode == 200,
            StatusCode = statusCode,
            Message = message,
            TotalCount=totalCount
        };
    }

    //public static OperationResult<TResult> FailureResult(string message,TResult result=default, int statusCode=400)
    //{
    //    return new OperationResult<TResult>{Result = result,ErrorMessage = message,IsSuccess = false,StatusCode=statusCode};
    //}
    public static OperationResult<TResult> FailureResult(string message =null, int statusCode = 400, string errorCode = null)
    {
        if (errorCode!=null && message==null)
        {
            message = ErrorMessages.GetMessage(errorCode);
        }
        return new OperationResult<TResult> {Message = message, IsSuccess = false, StatusCode = statusCode, ErrorCode = errorCode };
    }
    public static OperationResult<TResult> FailureResult(string[] messages, int statusCode = 400, string errorCode = null)
    {
        return new OperationResult<TResult>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            ErrorCode = errorCode,
            Message = string.Join("; ", messages) // Optional: also store combined message
        };
    }
    public static OperationResult<TResult> NotFoundResult(string message)
    {
        return new OperationResult<TResult> { ErrorMessage = message, IsSuccess = false, IsNotFound = true };
    }
}