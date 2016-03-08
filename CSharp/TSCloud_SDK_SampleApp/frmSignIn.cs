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
        TSCloud cloud_client = new TSCloud("http://api.3dsprint.com");
        
        public frmSignIn()
        {
            // Please check whether internet connection state is online or not on the other thread before authentication.
            if (!cloud_client.IsOnline())
            {
                MessageBox.Show("Please check internet connection");
            }

            InitializeComponent();
        }

        private void CreateFolderSample()
        {
            string Email = tbEmail.Text;
            string Password = tbPassword.Text;

            User user = cloud_client.Authenticate(Email, Password);

            if (User.IsValid(user))
            {
                TDSPRINT.Cloud.SDK.ModelClient model_client = new TDSPRINT.Cloud.SDK.ModelClient(cloud_client);
                Model created_folder = model_client.Create("folder_on_root", null);
                Model folder_in_folder = model_client.Create("folder_in_created_folder", created_folder.Id);

                TDSPRINT.Cloud.SDK.Datas.Model file = new TDSPRINT.Cloud.SDK.Datas.Model();
                file.Filepath = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                file.ParentId = (int?)created_folder.Id;
                file.Meta = new TDSPRINT.Cloud.SDK.Datas.Hash();
                file.Meta.Add("test", "test_meta");
                
                model_client.Create(file);
            }
        }

        private void doSigninCentercode()
        {
            string UserID = tbEmail.Text;
            string Password = tbPassword.Text;

            Newtonsoft.Json.Linq.JObject result = cloud_client.AuthenticateCenterCode(UserID, Password);
            MessageBox.Show(result.ToString());
        }

        private void doSignin()
        {
            string Email = tbEmail.Text;
            string Password = tbPassword.Text;

            User user = cloud_client.Authenticate(Email, Password);
            
            // Get Desktop Settings
            //Hash settings = TSCloud.GetDesktopSettings();

            //if (settings == null)
            //{
            //    settings = new Hash();
            //    settings.Add("last_signed_in", null);
            //}
            
            //// Update Desktop Settings
            //settings["last_signed_in"] = Convert.ToString(DateTime.Now);
            //Hash updated_settings = TSCloud.UpdateDesktopSettings(settings);
            
            if (User.IsValid(user))
            {
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
            CreateFolderSample();
            //doSignin();
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
