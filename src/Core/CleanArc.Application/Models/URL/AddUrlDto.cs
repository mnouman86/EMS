using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.URL
{
    public class AddUrlDto
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
    }
}
