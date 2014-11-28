using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TDSPRINT.Cloud.SDK;
using TDSPRINT.Cloud.SDK.Datas;

namespace TSCloud_SampleApp
{
    public partial class frmSignIn : Form
    {
        TSCloud TcClient = new TSCloud();

        public frmSignIn()
        {
            InitializeComponent();
        }

        private void doSignin()
        {
            string Email = tbEmail.Text;
            string Password = tbPassword.Text;

            if (User.IsValid(TcClient.authenticate(Email, Password)))
            {
                frmModelList FileList = new frmModelList(TcClient);
                FileList.Show();
                this.Hide();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            doSignin();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            doSignin();
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            doSignin();
        }
    }
}
