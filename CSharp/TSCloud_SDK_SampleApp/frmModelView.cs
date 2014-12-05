using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

using TDSPRINT.Cloud.SDK;
using TDSPRINT.Cloud.SDK.Datas;
using System.Threading;

namespace TSCloud_SampleApp
{
    public partial class frmModelView : Form
    {
        private string Filepath = null;
        private ModelClient ModelClient;

        public delegate void UpdateList(Object obj);
        public event UpdateList FormSendEvent;

        private int ModelId;
        private int m_Selected_Target_Folder;
        Thread DownloadThread;

        public int Selected_Target_Folder
        {
            get { return m_Selected_Target_Folder; }
            set { m_Selected_Target_Folder = value; }
        }

        public frmModelView(ModelClient tpModelClient, Model model)
        {
            InitializeComponent();

            if(model == null)
            {
                if(MessageBox.Show("Model is invalid") == DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }

            tbId.Text = model.Id.ToString();
            ModelId = model.Id;
            tbOwner.Text = model.Owner.Name;
            tbFilename.Text = !String.IsNullOrEmpty(model.Name) ? model.Name.ToString() : String.Empty;
            tbKey.Text = !String.IsNullOrEmpty(model.Key) ? model.Key.ToString() : String.Empty;
            tbFtype.Text = model.Ftype.ToString();
            tbMeta.Text = model.Meta != null ? model.Meta.ToString() : String.Empty;
            tbAcl.Text = model.Acl.ToString();

            ModelClient = tpModelClient;
            
            this.Text = model.Name;
            btnUpdate.Enabled = false;

            tbAcl.ScrollBars = ScrollBars.Vertical;
            tbMeta.ScrollBars = ScrollBars.Vertical;

            progressBar.Style = ProgressBarStyle.Blocks;
            CheckForIllegalCrossThreadCalls = false;
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
            int ModelId = !String.IsNullOrWhiteSpace(tbId.Text) ? Int32.Parse(tbId.Text) : 0;
            string ModelName = !String.IsNullOrWhiteSpace(tbFilename.Text) ? tbFilename.Text : null;
            string Meta = !String.IsNullOrWhiteSpace(tbMeta.Text) && !tbMeta.Text.Equals("{}") ? tbMeta.Text.Replace("\r\n","") : null;
            string Acl = !String.IsNullOrWhiteSpace(tbAcl.Text) ? tbAcl.Text.Replace("\r\n", "") : null;
            
            Model update_result = ModelClient.Update(ModelId, ModelName, Filepath, Meta, Acl);

            if(update_result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Filepath = null;
                this.FormSendEvent(1);
                
                if(MessageBox.Show("Model has successfully updated") == DialogResult.OK)
                    this.Close();
            }
            else
            {
                MessageBox.Show(String.Format("Model updating has failed with reason {0}.", update_result.Message));
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;

            if(dlgUploadModel.ShowDialog() == DialogResult.OK)
            {
                Filepath = dlgUploadModel.FileName;
                tbFilename.Text = Path.GetFileName(Filepath);
            }
        }

        private void UpdateProgress(float fPercent)
        {
            progressBar.Value = Convert.ToInt32(fPercent);
        }

        private void Download()
        {
            if (ModelClient.Download(ModelId, dlgDownloadModel.FileName, UpdateProgress) != HttpStatusCode.OK)
            {
                MessageBox.Show("Failed to download file");
            }
            else
            {
                DialogResult result = MessageBox.Show("Successfully downloaded.");
                if (result == DialogResult.OK)
                {
                    progressBar.Value = 0;
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            int ModelId = (Int32.Parse(tbId.Text)); 
            dlgDownloadModel.FileName = tbFilename.Text;

            if(dlgDownloadModel.ShowDialog() == DialogResult.OK)
            {
                Model file = ModelClient.Get(ModelId);
                DownloadThread = new Thread(new ThreadStart(Download));
                DownloadThread.Start();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this model?", "TP 2.0", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int ModelId = Int32.Parse(tbId.Text);

                if (ModelClient.Delete(ModelId).StatusCode == HttpStatusCode.OK)
                {
                    if (MessageBox.Show(String.Format("Model '{0}' has deleted", tbFilename.Text)) == DialogResult.OK)
                    {
                        this.FormSendEvent(1);
                        this.Close();
                    }
                }
            }
        }

        private void frmModelView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            frmTarget target = new frmTarget(ModelClient);
            if (target.ShowDialog(this) == DialogResult.OK)
            {
                HttpStatusCode Result = ModelClient.Copy(ModelId, Selected_Target_Folder);

                if (Result == HttpStatusCode.OK)
                {
                    MessageBox.Show("File has copied to...");
                }

            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            frmTarget target = new frmTarget(ModelClient);
            if (target.ShowDialog(this) == DialogResult.OK)
            {
                HttpStatusCode Result = ModelClient.Move(ModelId, Selected_Target_Folder);

                if (Result == HttpStatusCode.OK)
                {
                    MessageBox.Show("File has moved to...");
                }
            }
        }
    }
}
