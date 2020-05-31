using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class RebalanceBudgetSO : GenericSystemOperation
    {
        public bool Done { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            if(broker.RebalanceOne(obj)==1)
            Done = true;
            else
            Done = false;
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
