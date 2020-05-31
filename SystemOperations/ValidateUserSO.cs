using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class ValidateUserSO : GenericSystemOperation
    {
        public User user { get; private set; }

        protected override void DoOperation(IDomainObject obj)
        {
            user = (User)broker.ReturnOne(obj);
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
