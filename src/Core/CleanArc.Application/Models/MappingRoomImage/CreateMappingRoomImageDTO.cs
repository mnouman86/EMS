using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingRoomImage
{
    public class CreateMappingRoomImageDTO
    {
        public int? RoomID { get; set; }
        public int? CategoryID { get; set; }
        public string? ImagePaths { get; set; }
        public string? ImageTitles { get; set; }
        public string? IsMains { get; set; }
        public int? CreatedBy { get; set; }
    }
}
