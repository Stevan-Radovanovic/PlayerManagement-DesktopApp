using AppController;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmBudget : Form
    {

        public List<Contract> contracts = new List<Contract>();
        GuiContBudget cont = new GuiContBudget();
        public FrmBudget()
        {
            InitializeComponent();
            cont.CleanUpDgv(label3, dgvBudget);
            
        }


        private void FrmBudget_Load(object sender, EventArgs e)
        {
            cont.PullMostPaidStaff(dgvBudget);
        }

        private void buttonNo_Click_1(object sender, EventArgs e)
        {
            cont.CloseForm(this);
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            cont.Rebalance(label3, dgvBudget);           
        }
    }
}
