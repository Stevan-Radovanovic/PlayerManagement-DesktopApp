using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class PullAllContractsSO : GenericSystemOperation
    {
        public List<Contract> Contracts { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            Contracts = broker.ReturnAll(obj).Cast<Contract>().ToList();
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
