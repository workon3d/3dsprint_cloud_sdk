using System;
using System.IO;
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
    public partial class frmModelList : Form
    {
        private TpModelClient ModelClient;
        private User CurrentUser;

        public frmModelList()
        {
            InitializeComponent();
            setListColumn();
        }

        public frmModelList(TP2 TpClient) : this()
        {
            ModelClient = new TpModelClient(TpClient);
            CurrentUser = TpClient.CurrentUser;
            getFileList();
        }

        private void getFileList()
        {
            List<Model> models = ModelClient.All();

            foreach(Model model in models)
            {
                insertModel(model);
            }
        }

        private void insertModel(Model model)
        {
            ListViewItem item = new ListViewItem(model.id.ToString());
            item.SubItems.Add(model.name);
            lvFileList.Items.Add(item);
        }

        private void setListColumn()
        {
            lvFileList.FullRowSelect = true;
            lvFileList.View = View.Details;
            lvFileList.BeginUpdate();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (dlgUploadModel.ShowDialog() == DialogResult.OK)
            {
                string FilePath = dlgUploadModel.FileName;

                Model result = ModelClient.Create(FilePath);
                insertModel(result);
            }
        }

        private void lvFileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvFileList.SelectedItems.Count == 1)
            {
                ListView lv = sender as ListView;
                int SelectRow = lv.SelectedItems[0].Index;

                Model model = ModelClient.Get(Int32.Parse(lv.SelectedItems[0].Text));

                frmModelView ModelView = new frmModelView(ModelClient, model);
                ModelView.Show();
            }
        }
    }
}
