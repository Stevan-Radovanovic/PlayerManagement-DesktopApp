using AppController;
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
    public partial class FrmShowAll : Form
    {
        public GuiContShowAll cont = new GuiContShowAll();

        public FrmShowAll()
        {
            InitializeComponent();
            cont.dataGrid(dgvShowAll);
           
        }

        private void FrmShowAll_Load(object sender, EventArgs e)
        {
            cont.loadStaff(dgvShowAll);

            
        }

       
    }
}
