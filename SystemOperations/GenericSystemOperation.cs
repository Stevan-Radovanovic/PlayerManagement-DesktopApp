using BrokerBase;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations
{
    public abstract class GenericSystemOperation
    {

        protected Broker broker = new Broker();
        protected abstract void DoOperation(IDomainObject obj);
        protected abstract void Validation(IDomainObject obj);

        public void LetsDoIt(IDomainObject obj)
        {
            try
            {
                Validation(obj);
                broker.OpenConnection();
                broker.BeginTransaction();
                DoOperation(obj);
                broker.CommitTransaction();
            }
            catch (Exception)
            {
                broker.RollbackTransaction();
                throw;
            }
            finally
            {
                broker.CloseConnection();
            }
        }

    }
}
