using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class EMpolyeeWithDepartmentSpecification : BaseSpecifications<Empolyee>
    {
        public EMpolyeeWithDepartmentSpecification()
        {
           Includes.Add(E=>E.Id);
        }
        public EMpolyeeWithDepartmentSpecification(int id): base(E=>E.Id==id)
        {
        

            Includes.Add(E => E.Id);
        }
    }
}
