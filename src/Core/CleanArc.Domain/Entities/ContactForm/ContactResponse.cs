using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ContactForm
{
    public class ContactResponse
    {
        public int ContactFormId { get; set; }
        public int FeedbackStatusId { get; set; } 
        public string Remards { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int CultureId { get; set; }
    }
}
