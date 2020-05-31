using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class SportsDirector : IDomainObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string userName;
    
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<Contract> contracts;

        public List<Contract> Contracts
        {
            get { return contracts; }
            set { contracts = value; }
        }

        public string ReturnAttributes()
        {
            throw new NotImplementedException();
        }

        public string ReturnClassName()
        {
            return "FootballDatabase.dbo.SportsDirector";
        }

        public string SetAttributeValues()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForFindOne()
        {
            return $"UserName LIKE '{UserName}' AND Password LIKE '{Password}'";
        }

        public string ReturnCriteriaForFindMultiple(string text)
        {
            throw new NotImplementedException();
        }

        public List<IDomainObject> ReturnList(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IDomainObject ReturnObject(SqlDataReader reader)
        {
            if (reader.Read()) { 
            SportsDirector sd =  new SportsDirector
            {
                Id = Convert.ToInt32(reader["SportsDirectorId"].ToString()),
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString()
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
