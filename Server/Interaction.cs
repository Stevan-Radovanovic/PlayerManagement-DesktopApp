using AppController;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Interaction
    {
        private Socket clientSocket;
        private NetworkStream stream;
        private BinaryFormatter formatter = new BinaryFormatter();

        public Interaction(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            stream = new NetworkStream(clientSocket);
        }

        public void Request ()
        {
            bool end = false;
            while (!end)
            {
               
                Response response = new Response();
                try
                {
                    Request request = (Request)formatter.Deserialize(stream);
                    switch (request.Operation)
                    {            
                        case Operation.CreateNewStaffMember:
                            Contract c = (Contract)request.Object;
                            Controller.Instance.CreateNewStaffMember(c.Member,c);
                            response.Message = "";
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.ChangeContracts:
                            List<Contract> ct = (List<Contract>)request.Object;
                            Controller.Instance.ChangeContracts(ct);
                            response.Message = "";
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.CreateNewUser:
                            User uuuu = (User)request.Object;
                            Controller.Instance.CreateNewUser(uuuu);
                            response.Message = "";
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.DeleteStaffMember:
                            StaffMember smd = (StaffMember)request.Object;
                            Controller.Instance.DeleteStaffMember(smd);
                            response.Message = "";
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.FindContractsFromStaffMember:
                            StaffMember sm = (StaffMember)request.Object;
                            List<Contract> contrS = Controller.Instance.FindContractsFromStaffMember(sm);
                            response.Message = "";
                            response.Object = contrS;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.PullAllContracts:
                            List<Contract> contr = Controller.Instance.PullAllContracts();
                            response.Message = "";
                            response.Object = contr;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.PullCountries:
                            List<Country> countries = Controller.Instance.PullCountries();
                            response.Message = "";
                            response.Object = countries;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.PullMostPaidStaff:
                            List<Contract> membersMoney = Controller.Instance.PullMostPaidStaff();
                            response.Message = "";
                            response.Object = membersMoney;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.PullStaff:                            
                            List<StaffMember> membersPulled = Controller.Instance.PullStaff();
                            response.Message = "";
                            response.Object = membersPulled;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.RebalanceBudget:
                            List<Contract> budget = (List<Contract>)request.Object;
                            Controller.Instance.RebalanceBudget(budget);
                            response.Message = "";
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.SearchStaff:
                            String criteria = request.Object.ToString();
                            List<StaffMember> members = Controller.Instance.SearchStaff(criteria);
                            response.Message = "";
                            response.Object = members;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.ValidateSportsDirector:
                            SportsDirector sd = (SportsDirector)request.Object;
                            SportsDirector sportsD = Controller.Instance.ValidateSportsDirector(sd.UserName, sd.Password);
                            response.Message = "";
                            response.Object = sportsD;
                            formatter.Serialize(stream, response);
                            break;

                        case Operation.ValidateUser:
                            User u = (User)request.Object;
                            User user = Controller.Instance.ValidateUser(u.UserName, u.Password);
                            response.Message = "";
                            response.Object = user;
                            formatter.Serialize(stream, response);
                            break;
                    }
                }
                catch (IOException)
                {
                    end = true;
                }
                catch (Exception e)
                {
                    response.Signal = Signal.Error;
                    response.Message = e.Message;
                    formatter.Serialize(stream, response);
                }
            }
        }

    }
}
