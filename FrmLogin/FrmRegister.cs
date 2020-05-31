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
    public partial class FrmRegister : Form
    {
        public GuiContRegister cont = new GuiContRegister();

        public FrmRegister()
        {
            InitializeComponent();
        }

        

        private void btnHide1_Click(object sender, EventArgs e)
        {
            cont.ActivatePswrd1(txtPswrd);   
        }

        private void btnHide2_Click(object sender, EventArgs e)
        {
            cont.ActivatePswrd2(txtConfirm);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            cont.Register(txtConfirm,txtFirst,txtLast,txtMail,txtPswrd,txtUser,this);
            
        }
    }
}
