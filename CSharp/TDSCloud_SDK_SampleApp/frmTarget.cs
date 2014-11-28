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
using TDSPRINT.Cloud.SDK.Types;

namespace TSCloud_SampleApp
{
    public partial class frmTarget : Form
    {
        private int m_SelectedFolder;
        private TcModelClient ModelClient;
        private List<Model> ModelList = new List<Model>();

        public int SelectedModel
        {
            get { return m_SelectedFolder; }
            set { m_SelectedFolder = value; }
        }
        public frmTarget(TcModelClient tpModelClient)
        {
            InitializeComponent();
            ModelClient = tpModelClient;
            LoadModels();
        }

        private void LoadModels()
        {
            List<Model> models = ModelClient.All(Ftype.Folder);
            foreach(Model model in models)
            {
                cbFolderList.Items.Add(model.name);
                ModelList.Add(model);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SelectedModel = ModelList[cbFolderList.SelectedIndex].id;
            //((frmModelView)(this.Owner)).Selected_Target_Folder = SelectedModel;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
