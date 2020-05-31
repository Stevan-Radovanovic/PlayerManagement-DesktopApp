using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class PullContriesSO : GenericSystemOperation
    {
        public List<Country> Countries { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            Countries = broker.ReturnAll(obj).Cast<Country>().ToList();
        }

        protected override void Validation(IDomainObject obj)
        {
            if (!(obj is Country))
            {
                throw new ArgumentException();
            }
        }
    }
}
