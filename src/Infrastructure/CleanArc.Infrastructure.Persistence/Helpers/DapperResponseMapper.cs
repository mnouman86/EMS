using CleanArc.Domain.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Helpers
{
    public static class DapperResponseMapper
    {
        public static ResponseEntity MapToResponseEntity(this DynamicParameters parameters)
        {
            return new ResponseEntity
            {
                RecordID = parameters.Get<string>("@RecordId"),
                Code = parameters.Get<int>("@Code"),
                Message = parameters.Get<string>("@Message"),
                IsSuccess = parameters.Get<int>("@Code") == 1
            };
        }
    }
}
