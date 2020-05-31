using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmLogin
{
    public class Session
    {

        private static Session instance;

        public static Session Instance
        {
            get
            {
                if (instance == null) instance = new Session();
                return instance;
            }
        }

        private Session()
        {

        }

        public User user { get; set; }
        public SportsDirector sportsDirector { get; set; }
    }
}
