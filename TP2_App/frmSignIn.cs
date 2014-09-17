using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TeamPlatform.TP2_SDK;

namespace TP2_App
{
    public partial class frmSignIn : Form
    {
        TP2 TpClient = new TP2();

        public frmSignIn()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string Email = tbEmail.Text;
            string Password = tbPassword.Text;

            if (TpClient.authenticate(Email, Password))
            {
                //MessageBox.Show(String.Format("Authenticated! \r API TOKEN : {0}", TpClient.ApiToken));

                frmFileList FileList = new frmFileList();
                FileList.ApiToken = TpClient.ApiToken;
                FileList.Show();
                this.Hide();
            }
        }
    }
}
