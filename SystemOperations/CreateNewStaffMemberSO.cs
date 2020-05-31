using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SystemOperations
{
    public class CreateNewStaffMemberSO : GenericSystemOperation
    {
        public bool Done { get; private set; }
        public Contract C {get; set;}

        protected override void DoOperation(IDomainObject obj)
        {
            int res = broker.InsertOne(obj);
            if (res!=0)
            {
                Done = true;
            } else
            {
                Done = false;
                return;
            }

            C.Member = new StaffMember();
            C.Member.Id = res;

            if (broker.SaveComplex(C))
                Done = true;
            else
                Done = false;
        }

        protected override void Validation(IDomainObject obj)
        {
           if(!(obj is StaffMember))
            {
                throw new ArgumentException();
            }
        }
    }
}
