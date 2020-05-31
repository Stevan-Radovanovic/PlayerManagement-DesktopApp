using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContMain
    {
        internal void Differentiate(Button btnRebalance, Button btnNew)
        {
            if (Session.Instance.sportsDirector == null)
            {
                btnRebalance.Hide();
                btnNew.Hide();
            }
        }

        internal void ClickNew()
        {
            FrmNew frm = new FrmNew();
            frm.ShowDialog();
        }

        internal void ClickRebalance()
        {
            FrmBudget frm = new FrmBudget();
            frm.ShowDialog();
        }

        internal void ClickShowAll()
        {
            FrmShowAll frm = new FrmShowAll();
            frm.ShowDialog();
        }

        internal void ClickSearch()
        {
            FrmSearch frm = new FrmSearch();
            frm.ShowDialog();
        }
    }
}
