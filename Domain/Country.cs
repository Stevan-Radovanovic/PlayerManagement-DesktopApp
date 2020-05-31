using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Country : IDomainObject
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
                return Name;
        }

        public string ReturnAttributes()
        {
            throw new NotImplementedException();
        }

        public string ReturnClassName()
        {
            return "FootballDatabase.dbo.Country";
        }

        public string SetAttributeValues()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForFindOne()
        {
            return $"CountryId = {Id}";
        }

        public string ReturnCriteriaForFindMultiple(string text)
        {
            throw new NotImplementedException();
        }

        public List<IDomainObject> ReturnList(SqlDataReader reader)
        {
            List<IDomainObject> list = new List<IDomainObject>();
            while (reader.Read())
            {
                Country c = new Country();
                c.Id = reader.GetInt32(0);
                c.Name = reader.GetString(1);
                list.Add(c);
            }
            return list;
        }

        public IDomainObject ReturnObject(SqlDataReader reader)
        {
            if (reader.Read())
            {
                Country sd = new Country
                {
                    Id = Convert.ToInt32(reader["CountryId"].ToString()),
                    Name = reader["CountryName"].ToString()
                };
                return sd;
            }
            else
            {
                return null;
            }
        }

        public IDomainObject ReturnNested()
        {
            return null;
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
