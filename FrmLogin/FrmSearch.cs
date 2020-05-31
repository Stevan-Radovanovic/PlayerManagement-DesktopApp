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
    public partial class FrmSearch : Form
    {
        public BindingList<StaffMember> staffMember = new BindingList<StaffMember>();
        public GuiContSearch cont = new GuiContSearch();

        public FrmSearch()
        {
            InitializeComponent();
            cont.FixTheForm(staffMember, dgvSearch,btnDetails,btnTerminate);
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cont.Search(txtSearch,dgvSearch);            
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
           
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            cont.ShowOne(dgvSearch);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cont.DeleteStaff(dgvSearch, staffMember);            
        }
    }
}
