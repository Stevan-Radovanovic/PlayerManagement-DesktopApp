using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace FrmLogin
{
    public class GuiContShowOne
    {
        internal void Close(FrmShowOne frmShowOne)
        {
            frmShowOne.Close();
        }

        internal void ChangeContracts(BindingList<Contract> bindContracts)
        {
            List<Contract> contracts = new List<Contract>();
            contracts = bindContracts.ToList();

            try
            {
                Communication.Instance.ChangeContracts(contracts);
                MessageBox.Show("The contracts have not been changed");
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Something went wrong");
            }

        }

        internal void fillDgv(StaffMember s, DataGridView dataGridView1, BindingList<Contract> bindContracts)
        {
            List<Contract> contracts = Communication.Instance.FindContractsFromStaffMember(s);

            foreach (Contract con in contracts)
            {
                bindContracts.Add(con);
            }

            dataGridView1.DataSource = bindContracts;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }
    }
}
