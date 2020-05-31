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
    public class GuiContSearch
    {
        internal void FixTheForm(BindingList<StaffMember> staffMember, DataGridView dgvSearch, Button btnDetails, Button btnTerminate)
        {
            List<StaffMember> staffMembers = Communication.Instance.PullStaff();
            foreach (StaffMember s in staffMembers)
            {
                staffMember.Add(s);
            }
            dgvSearch.DataSource = staffMember;
            dgvSearch.Columns[3].Visible = false;
            dgvSearch.ReadOnly = true;

            if (Session.Instance.sportsDirector == null)
            {
                btnDetails.Hide();
                btnTerminate.Hide();
            }
        }

        internal void Search(TextBox txtSearch, DataGridView dgvSearch)
        {
            string criteria = txtSearch.Text.ToString();
            if (criteria.Equals("")) return;
            dgvSearch.DataSource = Communication.Instance.SearchStaff(criteria);
            if (dgvSearch.Rows.Count == 0) MessageBox.Show("No players match the criteria");
        }

        internal void DeleteStaff(DataGridView dgvSearch, BindingList<StaffMember> staffMember)
        {

            if (dgvSearch.SelectedRows.Count > 0)
            {
                StaffMember staff = (StaffMember)dgvSearch.SelectedRows[0].DataBoundItem;
                try
                {
                    Communication.Instance.DeleteStaffMember(staff);
                    MessageBox.Show("All contracts with this player have been terminated");
                    staffMember.Remove(staff);
                    dgvSearch.Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("We couldn't terminate all contracts with this player");
                }

            }
        }

        internal void ShowOne(DataGridView dgvSearch)
        {
            if (dgvSearch.SelectedRows.Count > 0)
            {
                StaffMember staff = (StaffMember)dgvSearch.SelectedRows[0].DataBoundItem;
                FrmShowOne show = new FrmShowOne(staff);
                show.ShowDialog();
            }
        }


    }
}
