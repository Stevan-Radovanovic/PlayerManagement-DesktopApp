using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Contract : IDomainObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private decimal contractAmount;

        public decimal ContractAmount
        {
            get { return contractAmount; }
            set { contractAmount = value; }
        }

        private DateTime dateOfExpiry;

        public DateTime DateOfExpiry
        {
            get { return dateOfExpiry; }
            set { dateOfExpiry = value; }
        }

        private List<Bonus> bonuses;

        public List<Bonus> Bonuses
        {
            get { return bonuses; }
            set { bonuses = value; }
        }

        private StaffMember member;

        public StaffMember Member
        {
            get { return member; }
            set { member = value; }
        }

        private SportsDirector director;

        public SportsDirector Director
        {
            get { return director; }
            set { director = value; }
        }

        public string ReturnAttributes()
        {
            return $"{ContractAmount},'{DateOfExpiry}',{Director.Id},{Member.Id}";
        }

        public string ReturnClassName()
        {
            return "FootballDatabase.dbo.Contract";
        }

        public string SetAttributeValues()
        {
            return $"WageAmount = (WageAmount * 80) / 100";
        }

        public string ReturnCriteriaForFindOne()
        {
            return $"ContractId = {Id}";
        }

        public string ReturnCriteriaForFindMultiple(string text)
        {
            return $"StaffMemberId = {Convert.ToInt32(text)}";
        }

        public List<IDomainObject> ReturnList(SqlDataReader reader)
        {
            List<IDomainObject> list = new List<IDomainObject>();
            while (reader.Read())
            {
                list.Add(new Contract()
                {
                    Id = Convert.ToInt32(reader["ContractId"]),
                    DateOfExpiry = Convert.ToDateTime(reader["DateOfTermination"]),
                    ContractAmount = Convert.ToDecimal(reader["WageAmount"]),
                    Member = new StaffMember(){
                        Id = Convert.ToInt32(reader["StaffMemberId"])
                    }
            }); 
            }

            return list;
        }

        public IDomainObject ReturnObject(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public IDomainObject ReturnNested()
        {
            return Member;
        }

        public void SetNested(IDomainObject domainObject)
        {
            Member = (StaffMember)domainObject;
        }

        public object ReturnPrimaryKey()
        {
            return "inserted.ContractId";
        }

        public IEnumerable<IDomainObject> ReturnWeakObjects()
        {
            return Bonuses;
        }

        public string ReturnOrderBy()
        {
            return $"WageAmount DESC";
        }

        public string SetAttributeValuesUpdate()
        {
            return $"WageAmount = {ContractAmount}, DateOfTermination = '{DateOfExpiry.ToShortDateString()}'";
        }

        public string ReturnCriteriaForUpdate()
        {
            return $"ContractId = {Id} AND StaffMemberId = {Member.Id}";
        }
    }
}
