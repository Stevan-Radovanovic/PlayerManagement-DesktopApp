using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FrmLogin
{
    public class Communication
    {

        private static Communication instance;
        private Socket clientSocket;
        private NetworkStream stream;
        private BinaryFormatter formatter = new BinaryFormatter();

        private Communication()
        {

        }

        public static Communication Instance
        {
            get
            {
                if (instance == null) instance = new Communication();
                return instance;
            }
        }

        public bool Connect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect("localhost", 9090);
                stream = new NetworkStream(clientSocket);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public List<StaffMember> SearchStaff(string criteria)
        {
            Request r = new Request() { Operation = Operation.SearchStaff, Object = criteria };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if (res.Signal == Signal.Ok)
            {
                return (List<StaffMember>)res.Object;
            }
            else
            {
                return null;
            }
        }

        public List<Contract> FindContractsFromStaffMember(StaffMember selectedStaff)
        {
            Request r = new Request() { Operation = Operation.FindContractsFromStaffMember,Object=selectedStaff };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if (res.Signal == Signal.Ok)
            {
                return (List<Contract>)res.Object;
            }
            else
            {
                return null;
            }
        }

        public List<Contract> PullAllContracts()
        {
            Request r = new Request() { Operation = Operation.PullAllContracts };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if (res.Signal == Signal.Ok)
            {
                return (List<Contract>)res.Object;
            }
            else
            {
                return null;
            }
        }

        public bool RebalanceBudget(List<Contract> list)
        {
            Request r = new Request { Operation = Operation.RebalanceBudget, Object = list };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeContracts(List<Contract> contracts)
        {
            Request r = new Request { Operation = Operation.ChangeContracts, Object = contracts };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Contract> PullMostPaidStaff()
        {
            Request r = new Request() { Operation = Operation.PullMostPaidStaff };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if (res.Signal == Signal.Ok)
            {
                return (List<Contract>)res.Object;
            }
            else
            {
                return null;
            }
        }

        public List<StaffMember> PullStaff()
        {
            Request r = new Request() { Operation = Operation.PullStaff };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if (res.Signal == Signal.Ok)
            {
                return (List<StaffMember>)res.Object;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteStaffMember(StaffMember staff)
        {
            Request r = new Request { Operation = Operation.DeleteStaffMember, Object = staff };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateNewUser(User user)
        {
            Request r = new Request { Operation = Operation.CreateNewUser, Object = user };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User ValidateUser(string userName, string password)
        {
            User u = new User { UserName = userName, Password = password };
            Request r = new Request { Operation = Operation.ValidateUser, Object = u };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return (User)res.Object;
            }
            else
            {
                return null;
            }
        }

        public SportsDirector ValidateSportsDirector(string userName, string password)
        {
            SportsDirector s = new SportsDirector { UserName = userName, Password = password };
            Request r = new Request { Operation = Operation.ValidateSportsDirector, Object = s };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return (SportsDirector)res.Object;
            }
            else
            {
                return null;
            }
        }

        public bool CreateNewStaffMember(StaffMember s, Contract c)
        {
            c.Member = s;
            Request r = new Request { Operation = Operation.CreateNewStaffMember, Object = c };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);
            if (res.Signal == Signal.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Country> PullCountries()
        {
            Request r = new Request() { Operation = Operation.PullCountries };
            formatter.Serialize(stream, r);
            Response res = (Response)formatter.Deserialize(stream);

            if(res.Signal==Signal.Ok)
            {
                return (List<Country>)res.Object;
            }
            else
            {
                return null;
            }
        }

    }
}
