using AppController;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmNew : Form
    {
        public int bonusIdCounter = 1;
        public BindingList<Bonus> bonuses = new BindingList<Bonus>();
        public GuiContNew cont = new GuiContNew();
        public FrmNew()
        {
            InitializeComponent();
            cont.FillFields(txtAmount,txtBirth,txtDate,txtFullName);           
            cont.PullCountries(cmbCountry);           
            cont.DataGridStart(dataGridView1,bonuses);
           

        }

        public List<String> listOfPositions = new List<string>()
        {
            "Head Coach","Assistant Coach","GoalKeeper","Midfielder","Defender","Forward"
        };

        public List<String> listOfBonuses = new List<String>()
        {
            "Goal","Assist","Clean Sheet"
        };

        private void FrmNew_Load(object sender, EventArgs e)
        {
            cont.DropDown(cmbPosition, cmbBonus, listOfBonuses, listOfPositions);       
        }



        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            cont.CreateNewPlayer(txtAmount, txtBirth, txtBonus, txtDate, txtFullName,
                cmbCountry, cmbPosition, bonuses, dataGridView1, bonusIdCounter);
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cont.DeleteBonus(dataGridView1,bonuses);       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cont.AddBonus(dataGridView1, bonuses, txtBonus, rtbDesc, bonusIdCounter, cmbBonus);
        }
    }
}
