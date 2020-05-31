using BrokerBase;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOperations;

namespace AppController
{
    public class Controller
    {

        private Broker broker = new Broker();

        private static Controller _instance;

        public static Controller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        private Controller()
        {
        }

        public List<StaffMember> SearchStaff(string criteria)
        {
            GenericSystemOperation systemOperation = new SearchStaffSO();
            ((SearchStaffSO)systemOperation).criteria = criteria;
            systemOperation.LetsDoIt(new StaffMember());
            return ((SearchStaffSO)systemOperation).staff;
        }

        public List<Contract> FindContractsFromStaffMember(StaffMember selectedStaff)
        {
            GenericSystemOperation systemOperation = new FindContractsFromStaffMemberSO();
            ((FindContractsFromStaffMemberSO)systemOperation).criteria = selectedStaff.Id.ToString();
            systemOperation.LetsDoIt(new Contract());
            return ((FindContractsFromStaffMemberSO)systemOperation).contracts;
        }

        public List<Contract> PullAllContracts()
        {
            GenericSystemOperation systemOperation = new PullAllContractsSO();
            systemOperation.LetsDoIt(new Contract());
            return ((PullAllContractsSO)systemOperation).Contracts;
        }

        public bool RebalanceBudget(List<Contract> list)
        {
            foreach (Contract c in list)
            {
                GenericSystemOperation systemOperation = new RebalanceBudgetSO();
                systemOperation.LetsDoIt(c);
                if (((RebalanceBudgetSO)systemOperation).Done == false) return false;
            }

            return true;
        }

        public bool ChangeContracts(List<Contract> contracts)
        {
            foreach (Contract c in contracts)
            {
                GenericSystemOperation systemOperation = new ChangeContractsSO();
                systemOperation.LetsDoIt(c);
                if (((ChangeContractsSO)systemOperation).Done == false) return false;
            }

            return true;
        }

        public List<Contract> PullMostPaidStaff()
        {
            GenericSystemOperation systemOperation = new PullMostPaidStaffSO();
            systemOperation.LetsDoIt(new Contract());
            return ((PullMostPaidStaffSO)systemOperation).contracts;
        }

        public List<StaffMember> PullStaff()
        {
            GenericSystemOperation systemOperation = new PullStaffSO();
            systemOperation.LetsDoIt(new StaffMember());
            return ((PullStaffSO)systemOperation).Staff;
        }

        public bool DeleteStaffMember(StaffMember staff)
        {
            GenericSystemOperation systemOperation = new DeleteStaffMemberSO();
            systemOperation.LetsDoIt(staff);
            return ((DeleteStaffMemberSO)systemOperation).Done;
        }


        public bool CreateNewUser(User user)
        {
            GenericSystemOperation systemOperation = new CreateNewUserSO();
            systemOperation.LetsDoIt(user);
            return ((CreateNewUserSO)systemOperation).Done;
        }

        public User ValidateUser(string userName, string password)
        {
            GenericSystemOperation systemOperation = new ValidateUserSO();
            systemOperation.LetsDoIt(new User() { UserName = userName, Password = password });
            return ((ValidateUserSO)systemOperation).user;
        }

        public SportsDirector ValidateSportsDirector(string userName, string password)
        {
            GenericSystemOperation systemOperation = new ValidateSportsDirectorSO();
            systemOperation.LetsDoIt(new SportsDirector() {UserName = userName, Password = password});
            return ((ValidateSportsDirectorSO)systemOperation).sd;
        }

       //sta ovde sa c
        public bool CreateNewStaffMember(StaffMember s, Contract c)
        {

            GenericSystemOperation systemOperation = new CreateNewStaffMemberSO();
            ((CreateNewStaffMemberSO)systemOperation).C = c;
            systemOperation.LetsDoIt(s);
            return ((CreateNewStaffMemberSO)systemOperation).Done;
        }

        public List<Country> PullCountries ()
        {
            GenericSystemOperation systemOperation = new PullContriesSO();
            systemOperation.LetsDoIt(new Country());
            return ((PullContriesSO)systemOperation).Countries;
        }
    }
}
