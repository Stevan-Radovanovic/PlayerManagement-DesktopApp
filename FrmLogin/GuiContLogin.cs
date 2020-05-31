using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContLogin
    {
        internal void FillFields1(TextBox txtPswrd, TextBox txtUser)
        {
            txtPswrd.Text = "whatever";
            txtUser.Text = "root1";
        }

        internal void FillFields2(TextBox txtPswrd, ComboBox cmbType)
        {
            cmbType.SelectedIndex = 1;
            txtPswrd.PasswordChar = '*';
        }

        internal void LogIn(TextBox txtPswrd, TextBox txtUser, ComboBox cmbType, FrmLogin frmLogin)
        {
            string password = txtPswrd.Text;
            string userName = txtUser.Text;
            string type = cmbType.SelectedItem.ToString();

            if (string.IsNullOrEmpty(txtPswrd.Text) || string.IsNullOrEmpty(txtUser.Text) ||
                string.IsNullOrEmpty(cmbType.SelectedItem.ToString()))
            {
                MessageBox.Show("You've left some fields empty");
                return;
            }

            if (type.Equals("User") && Communication.Instance.ValidateUser(userName, password) != null)
            {
                MessageBox.Show("Log in successful!");

                Session.Instance.user = Communication.Instance.ValidateUser(userName, password);

                FrmMain main = new FrmMain();

                frmLogin.Hide();
                main.ShowDialog();
                return;
            }

            if (type.Equals("Sports Director") && Communication.Instance.ValidateSportsDirector(userName, password) != null)
            {
                MessageBox.Show("Log in successful!");

                Session.Instance.sportsDirector = 
                    Communication.Instance.ValidateSportsDirector(userName, password);

                FrmMain main = new FrmMain();

                frmLogin.Hide();
                main.ShowDialog();
                return;
            }

            MessageBox.Show("Log in unsuccessful");
        }
    }
}
