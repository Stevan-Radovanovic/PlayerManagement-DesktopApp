using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class PullStaffSO : GenericSystemOperation
    {
        public List<StaffMember> Staff { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            Staff = broker.ReturnAll(obj).Cast<StaffMember>().ToList();
        }

        protected override void Validation(IDomainObject obj)
        {
            if (!(obj is StaffMember))
            {
                throw new ArgumentException();
            }
        }
    }
}
