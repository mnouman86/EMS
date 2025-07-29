using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ContactFormQueries
{
    public static string Create_ContactForm => "usp_Create_ContactForm";
    public static string Update_ContactForm => "usp_Update_ContactForm";
    public static string Delete_ContactForm => "usp_Delete_ContactForm";
    public static string GetAll_ContactForm => "usp_GetAll_ContactForm";
    public static string GetByID_ContactForm => "usp_GetByID_ContactForm";


}
