using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContRegister
    {

        internal void ReloadTxt(TextBox txtPswrd, TextBox txtConfirm)
        {
            txtPswrd.Text = "";
            txtConfirm.Text = "";
        }

        internal void ActivatePswrd1(TextBox txtPswrd)
        {
            if (txtPswrd.PasswordChar != '*') txtPswrd.PasswordChar = '*';
            else txtPswrd.PasswordChar = '\0';
        }

        internal void ActivatePswrd2(TextBox txtConfirm)
        {
            if (txtConfirm.PasswordChar != '*') txtConfirm.PasswordChar = '*';
            else txtConfirm.PasswordChar = '\0';
        }

        internal void Register(TextBox txtConfirm, TextBox txtFirst, TextBox txtLast, TextBox txtMail, TextBox txtPswrd, TextBox txtUser, FrmRegister frmRegister)
        {
            User user = new User();
            user.Email = txtMail.Text;
            user.FullName = txtFirst.Text + " " + txtLast.Text;
            user.Password = txtPswrd.Text;
            user.UserName = txtUser.Text;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName)
                || string.IsNullOrEmpty(user.FullName))
            {
                MessageBox.Show("You've left some fields empty");
                return;
            }

            string x = user.Email;
            if (!(x.Contains('@') && x.Contains('.') && x.IndexOf('@') < x.IndexOf('.')
                && x.IndexOf('@') != 0 && x.IndexOf('.') != x.Length - 1))
            {
                MessageBox.Show("Your e-mail adress in non-existent");
                return;
            }

            if (user.Password.Length < 8)
            {
                MessageBox.Show("Password needs to be at least 8 characters long");
                ReloadTxt(txtPswrd,txtConfirm);
                return;
            }

            if (!user.Password.Equals(txtConfirm.Text))
            {
                MessageBox.Show("You haven't confirmed your password");
                ReloadTxt(txtPswrd, txtConfirm);
                return;
            }

            if (!Communication.Instance.CreateNewUser(user))
            {
                MessageBox.Show("This user name is taken");
                return;
            }

            MessageBox.Show("Your account has been created");
            frmRegister.Close();
        }
    }
}
