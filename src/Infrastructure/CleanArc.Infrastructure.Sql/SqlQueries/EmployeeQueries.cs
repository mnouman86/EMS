namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class EmployeeQueries
    {
        public static string Create_Employee => "usp_Create_Employee";
        public static string Update_Employee => "usp_Update_Employee";
        public static string Delete_Employee => "usp_Delete_Employee";
        public static string GetAll_Employee => "usp_GetAll_Employee";
        public static string GetById_Employee => "usp_GetById_Employee";
        public static string MarkLeft_Employee => "usp_MarkLeft_Employee";
        public static string AssignTeacherSubjects => "usp_AssignTeacherSubjects";
        public static string GetTeacherAssignments => "usp_GetTeacherAssignments";
        public static string AddEmployeeDocument => "usp_Add_EmployeeDocument";
        public static string GetEmployeeDocuments => "usp_Get_EmployeeDocuments";

        // Foundational extensions
        public static string Upsert_EmployeeSalary => "usp_Upsert_EmployeeSalary";
        public static string Get_CurrentEmployeeSalary => "usp_Get_CurrentEmployeeSalary";
        public static string Get_EmployeeSalaryHistory => "usp_Get_EmployeeSalaryHistory";
        public static string Issue_EmployeeAdvance => "usp_Issue_EmployeeAdvance";
        public static string Adjust_EmployeeAdvance => "usp_Adjust_EmployeeAdvance";
        public static string Get_EmployeeAdvances => "usp_Get_EmployeeAdvances";
    }
}
