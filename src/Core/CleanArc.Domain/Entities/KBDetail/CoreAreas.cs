using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.KBDetail
{
    public class CoreAreas
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? Type { get; set; }
        public string? ImagePath { get; set; }
        public List<KBMinimalDetail> KBMinimalDetails { get; set; }
    }
}
