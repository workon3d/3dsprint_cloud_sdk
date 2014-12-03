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
    public partial class frmCreateFolder : Form
    {
        private ModelClient ModelClient;
        private List<Model> ModelList = new List<Model>();
        private string m_FolderName;
        public string ModelName
        {
            get { return m_FolderName; }
            set { m_FolderName = value; }
        }
        private int m_SelectedFolder;
        public int ModelId
        {
            get { return m_SelectedFolder; }
            set { m_SelectedFolder = value; }
        }


        public frmCreateFolder(ModelClient tpModelClient)
        {
            ModelClient = tpModelClient;
            InitializeComponent();
            LoadModels();

        }

        private void LoadModels()
        {
            List<Model> models = ModelClient.All(Ftype.Folder);
            foreach (Model model in models)
            {
                cbModelList.Items.Add(model.Name);
                ModelList.Add(model);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cbModelList.SelectedIndex > -1)
            {
                Model selected_model = ModelList[cbModelList.SelectedIndex];
                ModelId = selected_model.Id;
            }
            else
            {
                ModelId = 0;
            }

            ModelName = tbModelName.Text;
                        
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
