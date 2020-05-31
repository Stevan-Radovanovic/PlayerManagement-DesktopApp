using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContBudget
    {

        internal void CleanUpDgv(Label label3, DataGridView dgvBudget)
        {
            label3.Text = ReturnTotalWages().ToString() + " $";

            dgvBudget.DataSource = Communication.Instance.PullMostPaidStaff();
            dgvBudget.ReadOnly = true;
            dgvBudget.Columns["Id"].Visible = false;
            dgvBudget.Columns["Director"].Visible = false;
            dgvBudget.Columns["Member"].HeaderText = "Player Name";
            dgvBudget.Columns["ContractAmount"].HeaderText = "Wages";
            dgvBudget.Columns["DateOfExpiry"].HeaderText = "Date of Expiry";
        }

        internal void CloseForm(FrmBudget frm)
        {
            frm.Close();
        }

        internal void PullMostPaidStaff(DataGridView dgvBudget)
        {
            dgvBudget.DataSource = Communication.Instance.PullMostPaidStaff();
        }

        internal void Rebalance(Label label3, DataGridView dgvBudget)
        {
            if (Communication.Instance.RebalanceBudget(Communication.Instance.PullMostPaidStaff()))
            {
                MessageBox.Show("Budget has been rebalanced");
            }
            else
            {
                MessageBox.Show("Budget has not been rebalanced");
            }

            dgvBudget.DataSource = Communication.Instance.PullMostPaidStaff();
            dgvBudget.Refresh();
            label3.Text = ReturnTotalWages().ToString() + "$";
        }

        internal object ReturnTotalWages()
        {
            List<Contract> contracts = Communication.Instance.PullAllContracts();
            decimal sum = 0;

            foreach (Contract c in contracts)
            {
                sum += c.ContractAmount;
            }

            return sum;
        }


    }
}
