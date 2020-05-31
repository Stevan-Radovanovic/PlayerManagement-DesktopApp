using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    [Serializable]
    public class Bonus : IDomainObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private decimal moneyAmount;

        public decimal MoneyAmount
        {
            get { return  moneyAmount; }
            set {  moneyAmount = value; }
        }

        private string btype;

        public string Btype
        {
            get { return btype; }
            set { btype = value; }
        }

        private string description;

        public string Description { get => description; set => description = value; }

        public string ReturnAttributes()
        {
            return $"{Id},'{Btype}',{MoneyAmount},'{Description}'";

        }

        public string ReturnClassName()
        {
            return "FootballDatabase.dbo.Bonus";
        }

        public string SetAttributeValues()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForFindOne()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForFindMultiple(string t)
        {
            throw new NotImplementedException();
        }

        public List<IDomainObject> ReturnList(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IDomainObject ReturnObject(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IDomainObject ReturnNested()
        {
            throw new NotImplementedException();
        }

        public void SetNested(IDomainObject domenskiObjekat)
        {
            throw new NotImplementedException();
        }

        public object ReturnPrimaryKey()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainObject> ReturnWeakObjects()
        {
            throw new NotImplementedException();
        }

        public string ReturnOrderBy()
        {
            throw new NotImplementedException();
        }

        public string SetAttributeValuesUpdate()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
