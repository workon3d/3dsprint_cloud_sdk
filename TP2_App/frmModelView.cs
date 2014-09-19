using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TeamPlatform.TP2_SDK;
using TeamPlatform.TP2_SDK.Object;

namespace TP2_App
{
    public partial class frmModelView : Form
    {
        private TpModelClient ModelClient;

        public frmModelView(TpModelClient tpModelClient, Model model)
        {
            InitializeComponent();

            tbId.Text = model.id.ToString();
            tbFilename.Text = model.name;
            tbKey.Text = model.key;
            tbFtype.Text = model.ftype.ToString();
            tbMeta.Text = model.meta.ToString();
            tbAcl.Text = model.acl.ToString();

            ModelClient = tpModelClient;
            
            this.Text = model.name;
            btnUpdate.Enabled = false;

            tbAcl.ScrollBars = ScrollBars.Vertical;
            tbMeta.ScrollBars = ScrollBars.Vertical;
        }

        private void tbId_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void tbFilename_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void tbKey_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void tbFtype_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void tbMeta_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void tbAcl_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ModelId = Int32.Parse(tbId.Text);
            string ModelName = tbFilename.Text;
            string Key = tbKey.Text;
            string Ftype = tbFtype.Text;
            string Meta = tbMeta.Text.Replace("\r\n","");
            string Acl = tbAcl.Text.Replace("\r\n","");

            Model update_result = ModelClient.Update(ModelId, ModelName, null, Meta, Acl);

            if (update_result.status_code == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Model has successfully updated");
                this.Close();
            }
            else
            {
                MessageBox.Show(String.Format("Model updating has failed with reason {0}", update_result.message));
            }
        }
    }
}
