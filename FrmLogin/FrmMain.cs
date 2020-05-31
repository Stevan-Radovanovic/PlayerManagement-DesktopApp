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
    public partial class FrmMain : Form
    {
        public GuiContMain cont = new GuiContMain();

        public FrmMain()
        {
            InitializeComponent();
            cont.Differentiate(btnRebalance, btnNew);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            cont.ClickShowAll();           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cont.ClickSearch();
        }

        private void btnRebalance_Click(object sender, EventArgs e)
        {
            cont.ClickRebalance();           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cont.ClickNew();
        }
    }
}
