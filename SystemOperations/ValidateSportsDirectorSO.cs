using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class ValidateSportsDirectorSO : GenericSystemOperation
    {
        public SportsDirector sd { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            sd = (SportsDirector)broker.ReturnOne(obj);
        }

        protected override void Validation(IDomainObject obj)
        {
            if (!(obj is SportsDirector))
            {
                throw new ArgumentException();
            }
        }
    }
}
