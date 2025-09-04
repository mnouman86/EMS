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
        public int Code { get; set; } = -1;
        public string Message { get; set; }=string.Empty;
        public string RecordID { get; set; }="-1";

    }

}
