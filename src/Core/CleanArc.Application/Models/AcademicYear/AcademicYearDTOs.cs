using System;

namespace CleanArc.Application.Models.AcademicYear
{
    public class CreateAcademicYearDTO
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UpdateAcademicYearDTO
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class SetCurrentAcademicYearDTO
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
    }
}
