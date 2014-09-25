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
    public partial class frmTarget : Form
    {
        private int m_SelectedFolder;
        private TpModelClient ModelClient;
        private List<Model> ModelList = new List<Model>();

        public int SelectedModel
        {
            get { return m_SelectedFolder; }
            set { m_SelectedFolder = value; }
        }
        public frmTarget(TpModelClient tpModelClient)
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
