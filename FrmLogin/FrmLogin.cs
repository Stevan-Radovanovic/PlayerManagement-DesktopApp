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
    public partial class FrmLogin : Form
    {
        public GuiContLogin cont = new GuiContLogin();

        public FrmLogin()
        {
            InitializeComponent();
            cont.FillFields1(txtPswrd, txtUser);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            cont.FillFields2(txtPswrd, cmbType);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cont.LogIn(txtPswrd,txtUser, cmbType, this);
            
        }

       
    }
}
