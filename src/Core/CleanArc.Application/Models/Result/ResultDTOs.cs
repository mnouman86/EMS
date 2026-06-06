namespace CleanArc.Application.Models.Result
{
    public class CreateResultSessionDTO
    {
        public string Name { get; set; }
        public int MaxWritten { get; set; }
        public int MaxOral { get; set; }
        public int MaxAttribute { get; set; }
        public decimal WeightWritten { get; set; }
        public decimal WeightOral { get; set; }
        public decimal WeightPerformance { get; set; }
        public int RoundingDecimals { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ConfigureGradeBandsDTO
    {
        public string BandsJson { get; set; }       // serialized array of bands
        public int UpdatedBy { get; set; }
    }

    public class EnterMarksDTO
    {
        public int ResultSessionId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherEmployeeId { get; set; }
        public string EntriesJson { get; set; }     // serialized per-student rows
        public int UpdatedBy { get; set; }
    }

    public class LockResultsDTO
    {
        public int ResultSessionId { get; set; }
        public int SchoolClassId { get; set; }
        public string Reason { get; set; }
        public int ActorUserId { get; set; }
    }

    public class ParentSearchDTO
    {
        public string StudentCode { get; set; }
        public string SecondFactor { get; set; }    // DOB ISO yyyy-MM-dd OR issued PIN
        public string ClientIp { get; set; }
    }

    public class MarksEntryGridRequest
    {
        public int ResultSessionId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherEmployeeId { get; set; }
    }

    public class ClassSheetRequest
    {
        public int ResultSessionId { get; set; }
        public int SchoolClassId { get; set; }
    }

    public class StudentCardRequest
    {
        public int ResultSessionId { get; set; }
        public int StudentId { get; set; }
    }
}
