using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IDomainObject
    {

        string ReturnAttributes();
        string ReturnClassName();
        string SetAttributeValues();
        string SetAttributeValuesUpdate();
        string ReturnCriteriaForFindOne();
        string ReturnCriteriaForUpdate();
        string ReturnCriteriaForFindMultiple(string criteria);
        List<IDomainObject> ReturnList(SqlDataReader reader);
        IDomainObject ReturnObject(SqlDataReader reader);
        IDomainObject ReturnNested();
        void SetNested(IDomainObject domenskiObjekat);
        object ReturnPrimaryKey();
        IEnumerable<IDomainObject> ReturnWeakObjects();
        string ReturnOrderBy();
    }
}
