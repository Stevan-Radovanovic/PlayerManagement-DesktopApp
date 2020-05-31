using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class CreateNewUserSO : GenericSystemOperation
    {
        public bool Done { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            if (broker.InsertOne(obj) == 1)
                Done = true;
            else
                Done = false;
        }

        protected override void Validation(IDomainObject obj)
        {
            if (!(obj is User))
            {
                throw new ArgumentException();
            }
        }
    }
}
