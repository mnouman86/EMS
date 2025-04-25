using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Common
{
    public class ListResponseWrapper<T>
    {
        public IReadOnlyList<T> Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
    }
}
