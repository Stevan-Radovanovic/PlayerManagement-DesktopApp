using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class SearchStaffSO : GenericSystemOperation
    {
        public List<StaffMember> staff { get; private set; }
        public string criteria { get; set; }

        protected override void DoOperation(IDomainObject obj)
        {
            staff = broker.ReturnAllCriteria(obj, criteria).Cast<StaffMember>().ToList();
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
