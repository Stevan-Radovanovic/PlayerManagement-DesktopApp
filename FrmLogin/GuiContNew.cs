using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace FrmLogin
{
    public class GuiContNew
    {
        internal void FillFields(TextBox txtAmount, TextBox txtBirth, TextBox txtDate, TextBox txtFullName)
        {
            txtAmount.Text = "2211";
            txtBirth.Text = "21.10.2019";
            txtDate.Text = "21.10.2019";
            txtFullName.Text = "Stevan R";
        }

        internal void PullCountries(ComboBox cmbCountry)
        {
            cmbCountry.DataSource = Communication.Instance.PullCountries();
        }

        internal void DataGridStart(DataGridView dataGridView1, System.ComponentModel.BindingList<Domain.Bonus> bonuses)
        {
            dataGridView1.DataSource = bonuses;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Bonus Type";
        }

        internal void DropDown(ComboBox cmbPosition, ComboBox cmbBonus, List<string> listOfBonuses, List<string> listOfPositions)
        {
            cmbPosition.DataSource = listOfPositions;
            cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBonus.DataSource = listOfBonuses;
            cmbBonus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        internal void CreateNewPlayer(TextBox txtAmount, TextBox txtBirth, TextBox txtBonus, TextBox txtDate, TextBox txtFullName, ComboBox cmbCountry, ComboBox cmbPosition, BindingList<Bonus> bonuses, DataGridView dataGridView1, int bonusIdCounter)
        {
            if (string.IsNullOrEmpty(txtFullName.Text)
                || string.IsNullOrEmpty(txtBirth.Text) || string.IsNullOrEmpty(txtAmount.Text)
               || string.IsNullOrEmpty(txtDate.Text))
            {
                MessageBox.Show("We could not add this player");
                return;
            }

            StaffMember s = new StaffMember();
            Contract c = new Contract();

            try
            {
                s.FullName = txtFullName.Text;
                s.Count = (Country)cmbCountry.SelectedItem;
                s.Pos = cmbPosition.SelectedItem.ToString();

                if (!DateTime.TryParseExact(txtBirth.Text, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime1)) throw new Exception();

                s.DateOfBirth = dateTime1;

                if (!DateTime.TryParseExact(txtDate.Text, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime2)) throw new Exception();

                c.DateOfExpiry = dateTime2;
                c.ContractAmount = Convert.ToDecimal(txtAmount.Text);
                c.Member = s;
                c.Bonuses = new List<Bonus>();
                c.Director = Session.Instance.sportsDirector;

                c.Bonuses = bonuses.ToList();

                if (Communication.Instance.CreateNewStaffMember(s, c))
                {
                    MessageBox.Show("A new player has been added!");
                    txtAmount.Clear();
                    txtBirth.Clear();
                    txtDate.Clear();
                    txtFullName.Clear();
                    bonuses.Clear();
                    dataGridView1.Refresh();
                    bonusIdCounter = 1;
                }
                else
                    MessageBox.Show("ERROR! Something went wrong, please try again later!");

            }
            catch (Exception)
            {
                MessageBox.Show("We could not add this player");
                // throw;
            }
        }

        internal void DeleteBonus(DataGridView dataGridView1, BindingList<Bonus> bonuses)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                bonuses.Remove((Bonus)dataGridView1.SelectedRows[0].DataBoundItem);
                dataGridView1.Refresh();
            }
        }

        internal void AddBonus(DataGridView dataGridView1, BindingList<Bonus> bonuses, TextBox txtBonus, RichTextBox rtbDesc, int bonusIdCounter, ComboBox cmbBonus)
        {
            if (string.IsNullOrEmpty(txtBonus.Text) || string.IsNullOrEmpty(rtbDesc.Text))
                return;

            Bonus b = new Bonus()
            {
                Btype = cmbBonus.SelectedItem.ToString(),
                MoneyAmount = Convert.ToDecimal(txtBonus.Text),
                Description = rtbDesc.Text,
                Id = bonusIdCounter
            };

            bonusIdCounter++;
            bonuses.Add(b);
        }
    }
}
