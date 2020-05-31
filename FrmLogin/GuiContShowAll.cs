using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContShowAll
    {
        internal void dataGrid(DataGridView dgvShowAll)
        {
            dgvShowAll.ReadOnly = true;
            
        }

        internal void loadStaff(DataGridView dgvShowAll)
        {
            dgvShowAll.DataSource = Communication.Instance.PullStaff();
            if(dgvShowAll.Rows.Count==0)
                MessageBox.Show("There are no players available");
           // dgvShowAll.Columns["Count"].Visible = false;
        }
    }
}
