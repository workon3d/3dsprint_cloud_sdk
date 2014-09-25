using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TeamPlatform.TP2_SDK;
using TeamPlatform.TP2_SDK.Datas;
using TeamPlatform.TP2_SDK.Types;

namespace TP2_App
{
    public partial class frmCreateFolder : Form
    {
        private TpModelClient ModelClient;
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


        public frmCreateFolder(TpModelClient tpModelClient)
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
                cbModelList.Items.Add(model.name);
                ModelList.Add(model);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cbModelList.SelectedIndex > -1)
            {
                Model selected_model = ModelList[cbModelList.SelectedIndex];
                ModelId = selected_model.id;
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
