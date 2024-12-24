using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.UserSignUpRewards;

public  class UserSignUpRewards

{

    public int? UserID { get; set; } // Foreign Key or reference to another table
    public int? RewardRulesID { get; set; } = 1;

}
