using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery
{
    public class GetAllRoomImagesQueryResult
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public int RoomID { get; set; }
        public bool IsMain { get; set; }
    }
}
