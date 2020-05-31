using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{


    [Serializable]
    public class StaffMember : IDomainObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }


        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string pos;

        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        private Country count;

        public Country Count
        {
            get { return count; }
            set { count = value; }
        }

        public override string ToString()
        {
            return this.fullName;
        }

        public string ReturnAttributes()
        {
           return $"'{FullName}','{DateOfBirth.ToShortDateString()}','{Pos}',{Count.Id}";
        }

        public string ReturnClassName()
        {
            return "FootballDatabase.dbo.StaffMember";
        }

        public string SetAttributeValues()
        {
            throw new NotImplementedException();
        }

        public string ReturnCriteriaForFindOne()
        {
            return $"StaffMemberId = {Id}";
        }

        public string ReturnCriteriaForFindMultiple(string criteria)
        {
            return $"FullName LIKE '%{criteria}%'";
        }

        public List<IDomainObject> ReturnList(SqlDataReader reader)
        {
            List<IDomainObject> list = new List<IDomainObject>();
            while(reader.Read())
            {
                list.Add(new StaffMember()
                {
                    Id = (int)reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Pos = reader["Position"].ToString(),
                    Count = new Country()
                    {
                        Id = (int)reader["CountryId"]
                    },
                    //Count.Name = reader.GetString(4);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
            });
            }

            return list;
        }

        public IDomainObject ReturnObject(SqlDataReader reader)
        {
            if (reader.Read())
            {
                StaffMember sd = new StaffMember
                {
                    Id = Convert.ToInt32(reader["StaffMemberId"].ToString()),
                    FullName = reader["FullName"].ToString()
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
            return Count;
        }

        public void SetNested(IDomainObject domainObject)
        {
            Count = (Country)domainObject;
        }

        public object ReturnPrimaryKey()
        {
            return "inserted.StaffMemberId";
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
