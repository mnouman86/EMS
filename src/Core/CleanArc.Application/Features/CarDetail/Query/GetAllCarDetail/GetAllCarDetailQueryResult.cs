using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarDetail.Query.GetAllCarDetail
{
    public class GetAllCarDetailQueryResult
    {
        public int ID { get; set; }
        public int BusinessID { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string PlateNumber { get; set; }
        public int NoOfSeat { get; set; }
        public int RentPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
