using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public class ResponseEntity
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }=string.Empty;
        public int RecordID { get; set; }=-1;
    }

}
