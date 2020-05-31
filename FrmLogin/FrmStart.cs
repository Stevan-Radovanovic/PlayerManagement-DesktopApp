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
    public partial class FrmStart : Form
    {
        public GuiContStart cont = new GuiContStart();

        public FrmStart()
        {
            InitializeComponent();
            cont.Connect(this);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cont.clickRegister();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cont.clickLogin();

        }
    }
}
