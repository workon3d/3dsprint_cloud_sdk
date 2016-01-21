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
        TSCloud TSCloud = new TSCloud("http://ec2-54-92-241-236.compute-1.amazonaws.com");
        
        public frmSignIn()
        {
            // Please check whether internet connection state is online or not on the other thread before authentication.
            if (!TSCloud.IsOnline())
            {
                MessageBox.Show("Please check internet connection");
            }

            InitializeComponent();
        }

        private void doSigninCentercode()
        {
            string UserID = tbEmail.Text;
            string Password = tbPassword.Text;

            Newtonsoft.Json.Linq.JObject result = TSCloud.AuthenticateCenterCode(UserID, Password);
            MessageBox.Show(result.ToString());
        }

        private void doSignin()
        {
//            doSigninCentercode();
//            return;

            string Email = tbEmail.Text;
            string Password = tbPassword.Text;

            User user = TSCloud.Authenticate(Email, Password);

            // Get Desktop Settings
            Hash settings = TSCloud.GetDesktopSettings();

            if (settings == null)
            {
                settings = new Hash();
                settings.Add("last_signed_in", null);
            }
            
            // Update Desktop Settings
            settings["last_signed_in"] = Convert.ToString(DateTime.Now);
            Hash updated_settings = TSCloud.UpdateDesktopSettings(settings);
            
            if (User.IsValid(user))
            {
                MessageBox.Show(user.ApiToken);
                //frmMain FileList = new frmMain(TSCloud);
                //FileList.Show();
                //this.Hide();
            }
            else
            {
                MessageBox.Show(user.Message);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            doSignin();
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doSignin();
            }
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
