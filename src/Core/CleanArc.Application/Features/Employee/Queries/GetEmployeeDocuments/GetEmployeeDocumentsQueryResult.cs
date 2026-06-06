using System;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeDocuments
{
    public class GetEmployeeDocumentsQueryResult
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long? FileSizeBytes { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
