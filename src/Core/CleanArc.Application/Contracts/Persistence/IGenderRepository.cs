using CleanArc.Domain.Entities.Gender;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IGenderRepository:IRepository<Gender>
{
   // Task CreateGender(Gender Gender);
}
