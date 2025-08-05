using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ContactForm
{
    public class CreateContactResponseDTO
    {
        // public int Id { get; set; }
        public int ContactFormId { get; set; }
        public int FeedbackStatusId { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
