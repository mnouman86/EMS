using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Common
{
    public class Result
    {
        public bool Succeeded { get; protected set; }
        public string[]? Errors { get; protected set; }

        public static Result Success() => new() { Succeeded = true };

        public static Result Failure(params string[] errors) => new()
        {
            Succeeded = false,
            Errors = errors
        };
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T data) => new()
        {
            Succeeded = true,
            Data = data
        };

        public new static Result<T> Failure(params string[] errors) => new()
        {
            Succeeded = false,
            Errors = errors
        };
    }
}
