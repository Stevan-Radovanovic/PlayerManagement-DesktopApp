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
    public partial class FrmShowOne : Form
    {

        public BindingList<Contract> bindContracts = new BindingList<Contract>();
        public StaffMember selectedStaff = new StaffMember();
        public GuiContShowOne cont = new GuiContShowOne();

        public FrmShowOne(StaffMember s)
        {
            
            InitializeComponent();
            cont.fillDgv(s, dataGridView1, bindContracts);           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cont.Close(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cont.ChangeContracts(bindContracts);            
        }
    }
}
