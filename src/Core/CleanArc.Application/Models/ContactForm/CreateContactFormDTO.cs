using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ContactForm
{
    public class CreateContactFormDTO
    {
        // public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int FeedbackSubjectTypeId { get; set; } 
        public string Message { get; set; } = string.Empty;
        public int? CultureId { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
