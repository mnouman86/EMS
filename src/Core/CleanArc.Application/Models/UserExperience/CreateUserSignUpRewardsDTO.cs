using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.UserSignUpRewards
{
    public class CreateUserSignUpRewardsDTO
    {
        public int? RewardRulesID { get; set; } = 1;

        public int? UserID { get; set; } // Foreign Key or reference to another table
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }

}
