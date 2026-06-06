using System;

namespace CleanArc.Application.Features.Result.Queries.GetResultSessions
{
    public class GetResultSessionsQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int MaxWritten { get; set; }
        public int MaxOral { get; set; }
        public int MaxAttribute { get; set; }
        public decimal WeightWritten { get; set; }
        public decimal WeightOral { get; set; }
        public decimal WeightPerformance { get; set; }
        public int RoundingDecimals { get; set; }
        public bool? IsConfigLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
