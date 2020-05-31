using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class PullMostPaidStaffSO : GenericSystemOperation
    {
        public List<Contract> contracts { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            contracts = broker.ReturnTop3(obj).Cast<Contract>().ToList();
        }

        protected override void Validation(IDomainObject obj)
        {
            if (!(obj is Contract))
            {
                throw new ArgumentException();
            }
        }
    }
}
