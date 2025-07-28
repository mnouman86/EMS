using CleanArc.Domain.Entities.ContactForm;
using CleanArc.Domain.Entities.CoreArea;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IContactFormRepository:IRepository<ContactForm>
{
   // Task CreateAgeType(AgeType ageType);
}
