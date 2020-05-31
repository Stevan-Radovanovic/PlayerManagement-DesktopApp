using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerBase
{
    public class Broker
    {
        
        private SqlConnection connection;
        private SqlTransaction transaction;

        public Broker()
        {
           // connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FootballDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public void BeginTransaction ()
        {
             transaction = connection.BeginTransaction();
        }

        public void CommitTransaction ()
        {
            transaction.Commit();
        }

        public void RollbackTransaction ()
        {
            transaction.Rollback();
        }

        public void OpenConnection ()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public int InsertOne(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);
            command.CommandText = $"insert into {obj.ReturnClassName()} output {obj.ReturnPrimaryKey()} values ({obj.ReturnAttributes()})";
            int id = (int)command.ExecuteScalar();
            return id;
        }

        public List<IDomainObject> ReturnAll(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);
            command.CommandText = $"select * from {obj.ReturnClassName()} ";
            SqlDataReader reader = command.ExecuteReader();
            List<IDomainObject> result = obj.ReturnList(reader);
            reader.Close();

            foreach (IDomainObject res in result)
            {
                Type t = obj.GetType();
                if (res.ReturnNested() == null)
                    break;
                res.SetNested(ReturnOne(res.ReturnNested()));
            }
            return result;

        }

        public List<IDomainObject> ReturnTop3(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);
            command.CommandText = $"select TOP 3 * from {obj.ReturnClassName()} ORDER BY {obj.ReturnOrderBy()} ";
            SqlDataReader reader = command.ExecuteReader();
            List<IDomainObject> result = obj.ReturnList(reader);
            reader.Close();

            foreach (IDomainObject res in result)
            {
                Type t = obj.GetType();
                if (res.ReturnNested() == null)
                    break;
                res.SetNested(ReturnOne(res.ReturnNested()));
            }
            return result;

        }

        public int DeleteOne(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand($"delete from {obj.ReturnClassName()} where {obj.ReturnCriteriaForFindOne()}", connection, transaction);
            return command.ExecuteNonQuery();
        }

        public int RebalanceOne(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand($"update {obj.ReturnClassName()} set {obj.SetAttributeValues()} where {obj.ReturnCriteriaForFindOne()}", connection, transaction);
            return command.ExecuteNonQuery();
        }

        public int UpdateOne(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand($"update {obj.ReturnClassName()} set {obj.SetAttributeValuesUpdate()} where {obj.ReturnCriteriaForFindOne()}", connection, transaction);
            return command.ExecuteNonQuery();
        }

        public List<IDomainObject> ReturnAllCriteria(IDomainObject obj, string text)
        {
            List<IDomainObject> result;

            SqlCommand command = new SqlCommand($"select * from {obj.ReturnClassName()} where {obj.ReturnCriteriaForFindMultiple(text)}", connection, transaction);
            SqlDataReader reader = command.ExecuteReader();
            result = obj.ReturnList(reader);
            reader.Close();

            foreach (IDomainObject res in result)
            {
                if (res.ReturnNested() == null)
                    break;
                res.SetNested(ReturnOne(res.ReturnNested()));
            }
            return result;

        }

        public IDomainObject ReturnOne(IDomainObject obj)
        {
            IDomainObject result;

            SqlCommand command = new SqlCommand($"select * from {obj.ReturnClassName()} where {obj.ReturnCriteriaForFindOne()}", 
                connection, transaction);
            SqlDataReader reader = command.ExecuteReader();
            result = obj.ReturnObject(reader);
            reader.Close();
            return result;
        }

        public bool SaveComplex(IDomainObject obj)
        {
            SqlCommand command = new SqlCommand($"insert into {obj.ReturnClassName()} output {obj.ReturnPrimaryKey()} values ({obj.ReturnAttributes()})", connection, transaction);

            int id = (int)command.ExecuteScalar();
            foreach (IDomainObject o in obj.ReturnWeakObjects())
            {
                SqlCommand command2 = new SqlCommand($"insert into {o.ReturnClassName()} values ({id}, {o.ReturnAttributes()})", connection, transaction);
                command2.ExecuteScalar();

            }
            return true;
        }

        /*
        public int ReturnMaxIdUser ()
        {
            try
            {
                //connection.Open();
                SqlCommand command = new SqlCommand();
                // connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT MAX(UserId) FROM FootballDatabase.dbo.[User]";
                object result = command.ExecuteScalar();
                if (result is DBNull)
                {
                    return 1;
                }
                int maxid = (int)result;
                return maxid + 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //connection.Close();
            }
        }
       
        
        public void ChangeContracts(List<Contract> contracts)
        {
            foreach(Contract c in contracts)
            {
                SqlCommand sql = new SqlCommand("UPDATE FootballDatabase.dbo.Contract SET" +
                    " WageAmount = @param1, DateOfTermination = @param2 WHERE ContractId = @param3 AND StaffMemberId = @param4", connection, transaction);
                sql.Parameters.AddWithValue("@param1", c.ContractAmount);
                sql.Parameters.AddWithValue("@param2", c.DateOfExpiry);
                sql.Parameters.AddWithValue("@param3", c.Id);
                sql.Parameters.AddWithValue("@param4", c.Member.Id);
                sql.ExecuteNonQuery();
            }
        }

       
        public void DeleteStaffMember(StaffMember staff)
        {
            try
            {
                SqlCommand sql = new SqlCommand("DELETE FROM FootballDatabase.dbo.StaffMember WHERE StaffMemberId = @param", connection);
                sql.Parameters.AddWithValue("@param", staff.Id);
                sql.ExecuteNonQuery();
              }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CreateNewUser(User user)
        {
            int flag;

            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO FootballDatabase.dbo.[User] VALUES (@Id," +
                    $"@Email,@User,@Pass,@Name)";
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Name", user.FullName);
                command.Parameters.AddWithValue("@User", user.UserName);
                command.Parameters.AddWithValue("@Pass", user.Password);
                command.Parameters.AddWithValue("@Id", ReturnMaxIdUser());
                flag = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return flag == 1;
        }

       
        public User ValidateUser(string userName, string password)
        {
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM FootballDatabase.dbo.[User]";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["UserId"].ToString());
                    string us = reader["UserName"].ToString();
                    string pass = reader["Password"].ToString();
                    if (us.Equals(userName) && pass.Equals(password)) return new User() { UserName=us, Password=pass, Id = id}; 
                }
                return null;
            }
            catch
            {
                 throw;
               // throw new Exception("An error emerged from our database, please try again later");
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }

            
        }

       
        public SportsDirector ValidateSportsDirector(string userName, string password)
        {
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM FootballDatabase.dbo.SportsDirector";
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    int id = Convert.ToInt32(reader["SportsDirectorId"].ToString());
                    string us = reader["UserName"].ToString();
                    string pass = reader["Password"].ToString();
                    if (us.Equals(userName) && pass.Equals(password)) return new SportsDirector() { UserName=us, Password=pass, Id = id};
                }
                return null;
            }
            catch
            {
               // throw;
               throw new Exception("An error emerged from our database, please try again later");
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }

            
        }

       
        public List<StaffMember> PullStaff()
        {
            List<StaffMember> l = new List<StaffMember>();
            SqlDataReader reader = null;

            try
            {
                //connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT S.StaffMemberId, S.FullName,S.Position," +
                    $"C.CountryId, C.CountryName, S.DateOfBirth FROM  FootballDatabase.dbo.StaffMember S JOIN " +
                    $"FootballDatabase.dbo.Country C ON (S.CountryId= C.CountryId)";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StaffMember s = new StaffMember();
                    s.Id = (int)reader.GetInt32(0);
                    s.FullName = reader.GetString(1);
                    s.Pos = reader["Position"].ToString();
                    s.Count = new Country();
                    s.Count.Id = reader.GetInt32(3);
                    s.Count.Name = reader.GetString(4);
                    s.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    l.Add(s);
                }
            }
            catch (Exception e)
            {
               throw new Exception("An error emerged from our database, please try again later");
            }
            finally
            {
               // connection.Close();
                if (reader != null) reader.Close();
            }
            return l;
        }

       
        public List<StaffMember> SearchStaff(string criteria)
        {
            List<StaffMember> l = new List<StaffMember>();
            SqlDataReader reader = null;
            
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT S.StaffMemberId, S.FullName,S.Position," +
                    $"C.CountryId, C.CountryName, S.DateOfBirth FROM FootballDatabase.dbo.StaffMember S JOIN " +
                    $"FootballDatabase.dbo.Country C ON (S.CountryId= C.CountryId) WHERE FullName LIKE '%{criteria}%'";
                //command.Parameters.AddWithValue("@Criteria", criteria);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    StaffMember s = new StaffMember();
                    s.Id = (int)reader.GetInt32(0);
                    s.FullName = reader.GetString(1);
                    //Datum!
                    s.Pos = reader["Position"].ToString();
                    s.Count = new Country();
                    s.Count.Id = reader.GetInt32(3);
                    s.Count.Name = reader.GetString(4);
                    s.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    l.Add(s);
                }
            }
            catch
            {
                throw new Exception("An error emerged from our database, please try again later");
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }
            return l;
        }

        
        public List<Contract> PullMostPaidStaff()
        {
            List<Contract> l = new List<Contract>();
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TOP 3 * FROM FootballDatabase.dbo.StaffMember S JOIN FootballDatabase.dbo.Contract C" +
                    " on (S.StaffMemberId=C.StaffMemberId) ORDER BY WageAmount DESC", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contract con = new Contract();
                    con.Id = Convert.ToInt32(reader["ContractId"]);
                    con.DateOfExpiry = Convert.ToDateTime(reader["DateOfTermination"]);
                    con.ContractAmount = Convert.ToDecimal(reader["WageAmount"]);
                    con.Member = new StaffMember();
                    con.Member.FullName = Convert.ToString(reader["FullName"]);
                    l.Add(con);
                }

                return l;
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }
            
        }
 
        public List<Contract> PullAllContracts()
        {
            List<Contract> l = new List<Contract>();
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM FootballDatabase.dbo.Contract C" +
                    " ", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contract con = new Contract();
                    con.Id = Convert.ToInt32(reader["ContractId"]);
                    con.DateOfExpiry = Convert.ToDateTime(reader["DateOfTermination"]);
                    con.ContractAmount = Convert.ToDecimal(reader["WageAmount"]);
                  
                    l.Add(con);
                }

                return l;
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            }

        }

      
        public void RebalanceBudget(List<Contract> contracts)
        {
            try
            {

                foreach(Contract c in contracts)
                {
                    SqlCommand command = new SqlCommand("UPDATE FootballDatabase.dbo.Contract SET " +
                        "WageAmount = (WageAmount*80)/100 WHERE ContractId = @Param", connection, transaction);
                    command.Parameters.AddWithValue("@Param", c.Id);
                    command.ExecuteNonQuery();
                }

            }
            catch
            {
                throw;
            }

        }

   
        public int GetSportsDirectorId (SportsDirector sd)
        {
            try
            {
                SqlCommand command = new SqlCommand($"SELECT SportsDirectorId FROM FootballDatabase.dbo.SportsDirector WHERE userName LIKE @name", connection, transaction);
              //  command.CommandText = $"SELECT SportsDirectorId FROM FootballDatabase.dbo." +
                //    $"SportsDirector WHERE userName LIKE @name";
                command.Parameters.AddWithValue("@name", sd.UserName);
                object result = command.ExecuteScalar();

                return (int)result;
            }
            catch
            {
                throw new Exception("An error emerged from our database, please try again later");
            }
            finally
            {
            //    connection.Close();
            }


        }

    
        public List<Country> PullCountries ()
        {
            List<Country> countries = new List<Country>();

            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM FootballDatabase.dbo.Country", connection, transaction);
                reader = command.ExecuteReader(); 

                while(reader.Read())
                {
                    Country c = new Country();
                    c.Id = reader.GetInt32(0);
                    c.Name = reader.GetString(1);
                    countries.Add(c);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                //Connection is not being closed here, because I'm using transactions
                if (reader != null) reader.Close();
            } 

            return countries;
        }

       
        public int FindOrCreateCountry(Country c) {

            List<Country> countries = PullCountries();

            foreach(Country country in countries)
            {
                if (country.Name.Trim().Equals(c.Name.Trim())) return country.Id;
            }

            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO FootballDatabase.dbo.Country VALUES" +
                    "(@Id,@Name)",connection,transaction);
                command.Parameters.AddWithValue("@Id",countries.Count + 1);
                command.Parameters.AddWithValue("@Name", c.Name);
                command.ExecuteNonQuery();
                return countries.Count + 1;
            }
            catch
            {
                throw;
            }
            finally
            {
               //     
            }

        }

        
        public void CreateNewStaffMember(StaffMember s, Contract c)
        {
            //I'm opening and closing the connection from the controller, like they showed in class

            try
            {
                c.Id = GetMaxContractId();
                s.Id = GetMaxStaffMemberId();
                s.Count.Id = FindOrCreateCountry(s.Count);


                SqlCommand commandStaffMember = new SqlCommand("INSERT INTO FootballDatabase.dbo.StaffMember VALUES" +
                    "(@Id,@Name,@Date,@Position,@CountryId)", connection, transaction);
                commandStaffMember.Parameters.AddWithValue("@Id", s.Id);
                commandStaffMember.Parameters.AddWithValue("@Name", s.FullName);
                commandStaffMember.Parameters.AddWithValue("@Date", s.DateOfBirth);
                commandStaffMember.Parameters.AddWithValue("@Position", s.Pos);
                commandStaffMember.Parameters.AddWithValue("@CountryId", s.Count.Id);

                commandStaffMember.ExecuteNonQuery();

                //Why are we using this constructor?
                SqlCommand commandContract = new SqlCommand("INSERT INTO FootballDatabase.dbo.Contract VALUES " +
                    "(@Id,@Amount,@Date,@Director,@Member)", connection, transaction);
                 commandContract.Parameters.AddWithValue("@Id", c.Id);
                commandContract.Parameters.AddWithValue("@Amount", c.ContractAmount);
                commandContract.Parameters.AddWithValue("@Date", c.DateOfExpiry);
                commandContract.Parameters.AddWithValue("@Director", GetSportsDirectorId(c.Director));
                commandContract.Parameters.AddWithValue("@Member", s.Id);
                commandContract.ExecuteNonQuery();
                foreach (Bonus b in c.Bonuses)
                {
                    SqlCommand commandBonus = new SqlCommand("INSERT INTO FootballDatabase.dbo.Bonus VALUES" +
                        "(@bid,@cid,@type,@amount,@description)", connection, transaction);
                    commandBonus.Parameters.AddWithValue("@cid", c.Id);
                    commandBonus.Parameters.AddWithValue("@bid", b.Id);
                    commandBonus.Parameters.AddWithValue("@type", b.Btype);
                    commandBonus.Parameters.AddWithValue("@amount", b.MoneyAmount);
                    commandBonus.Parameters.AddWithValue("@description", b.Description);
                    commandBonus.ExecuteNonQuery();
                }

            }
            catch
            {
                throw;
            }
        }

       
        public int GetMaxStaffMemberId()
        {
            try
            {
                //connection.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(StaffMemberId) FROM FootballDatabase.dbo.StaffMember",connection,transaction);
                // connection.CreateCommand();
               // command.Connection = connection;
                //command.CommandText = "SELECT MAX(StaffMemberId) FROM FootballDatabase.dbo.StaffMember";
                object result = command.ExecuteScalar();
                if (result is DBNull)
                {
                    return 1;
                }
                int maxid = (int)result;
                return maxid + 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //connection.Close();
            }
        }

      
        public int GetMaxContractId()
        {
            try
            {
                //connection.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(ContractId) FROM FootballDatabase.dbo.Contract",connection,transaction);
                // connection.CreateCommand();
              //  command.Connection = connection;
                //command.CommandText = "SELECT MAX(ContractId) FROM FootballDatabase.dbo.Contract";
                object result = command.ExecuteScalar();
                if (result is DBNull)
                {
                    return 1;
                }
                int maxid = (int)result;
                return maxid + 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //connection.Close();
            }
        }

        
        public List<Contract> FindContractsFromStaffMember(StaffMember selectedStaff)
        {

            List<Contract> l = new List<Contract>();
            SqlDataReader reader = null;

            try
            {

                SqlCommand command = new SqlCommand("Select * from FootballDatabase.dbo.Contract" +
                    " where StaffMemberId = @param", connection);
                command.Parameters.AddWithValue("@param", selectedStaff.Id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contract s = new Contract();
                    s.Id = Convert.ToInt32(reader["ContractId"].ToString());
                    s.ContractAmount = Convert.ToDecimal(reader["WageAmount"].ToString());
                    s.DateOfExpiry = Convert.ToDateTime(reader["DateOfTermination"]);
                    s.Member = new StaffMember();
                    s.Member.Id = selectedStaff.Id;
                    l.Add(s);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                // connection.Close();
                if (reader != null) reader.Close();
            }
            return l;
        }*/
    }
}
