using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3DSPRINTCloudDB;
using TDSPRINT.Cloud.SDK;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTStub
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {

            //sc.ExecuteCommand((int)YourMethods.methodX);

            CloudDBAgent agent = new CloudDBAgent();
            {
                using (TDSPRINT.Cloud.SDK.TSCloud TsCloud = new TDSPRINT.Cloud.SDK.TSCloud("http://api-staging.3dsprint.com"))
                {
                    TDSPRINT.Cloud.SDK.Datas.User user = TsCloud.Authenticate(email.Text, password.Text);

                    JObject env = new JObject();
                    env["api_host"] = TsCloud.ApiHost;
                    env["agent_log_location"] = @"c:\work\fake_printer.dat";
                    bool result, result2;
                    result = agent.UpdateEnv(env);
                    result2 = agent.UpdateUserInfo(user.Email, user.ApiToken, user.RefreshToken, user.TokenExpiration);

                    MessageBox.Show(string.Format("UpdateEnv: {0}\nUpdateUserInfo: {1}\n", result, result2));
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
