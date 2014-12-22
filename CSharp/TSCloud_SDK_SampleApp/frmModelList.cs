using System;
using System.IO;
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
    public partial class frmModelList : Form
    {
        private ModelClient ModelClient;
        private User CurrentUser;

        public int SelectedFolder;

        public frmModelList()
        {
            InitializeComponent();
            setListColumn();
        }

        public frmModelList(TSCloud TSCloud) : this()
        {
            ModelClient = new ModelClient(TSCloud);
            CurrentUser = TSCloud.CurrentUser;
            getFileList();
        }

        public void updateFileList(Object obj)
        {
            lvFileList.Items.Clear();
            getFileList();
        }
        
        #region private method
        private void getFileList()
        {
            List<Model> models = ModelClient.All();

            foreach (Model model in models)
            {
                insertModel(model);
            }
        }
        private void insertModel(Model model)
        {
            ListViewItem item = new ListViewItem(model.Id.ToString());
            item.SubItems.Add(model.Name);
            try
            {
                string strSerializedMeta = model.Meta.Stringify();
                item.SubItems.Add(strSerializedMeta);
            }
            catch { }
            lvFileList.Items.Add(item);
        }
        private void setListColumn()
        {
            lvFileList.FullRowSelect = true;
            lvFileList.View = View.Details;
            lvFileList.BeginUpdate();
        }
        private void doSearch()
        {
            lvFileList.Items.Clear();
            List<Model> models = ModelClient.Find(tbQuery.Text);

            foreach (Model model in models)
            {
                insertModel(model);
            }
        }
        private void showItem(object sender)
        {
            if (lvFileList.SelectedItems.Count == 1)
            {
                ListView lv = sender as ListView;
                int SelectRow = lv.SelectedItems[0].Index;

                Model model = ModelClient.Get(Int32.Parse(lv.SelectedItems[0].Text));
                
                frmModelView ModelView = new frmModelView(ModelClient, model);
                ModelView.FormSendEvent += new frmModelView.UpdateList(updateFileList);
                ModelView.Show();
            }
        }
        #endregion

        #region Event
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (dlgUploadModel.ShowDialog() == DialogResult.OK)
            {
                string FilePath = dlgUploadModel.FileName;

                Model result = ModelClient.Create(FilePath);
                updateFileList(null);
            }
        }
        private void lvFileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showItem(sender);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            doSearch();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            updateFileList(1);
        }

        private void tbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doSearch();
            }

            if (e.KeyCode == Keys.Escape)
            {
                ((TextBox)sender).Text = String.Empty;
                updateFileList(1);
            }
        }

        private void lvFileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showItem(sender);
            }
        }
        #endregion

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            frmCreateFolder target = new frmCreateFolder(ModelClient);
            if (target.ShowDialog(this) == DialogResult.OK)
            {
                Model folder = ModelClient.Create(target.ModelName, target.ModelId);

                if (Model.IsValid(folder))
                {
                }
            }
        }
    }
}
