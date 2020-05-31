using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class FindContractsFromStaffMemberSO : GenericSystemOperation
    {
        public List<Contract> contracts { get; private set; }
        public string criteria { get;  set; }

        protected override void DoOperation(IDomainObject obj)
        {
            contracts = broker.ReturnAllCriteria(obj, criteria)
                .Cast<Contract>().ToList();
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
