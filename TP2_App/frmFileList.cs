using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TeamPlatform.TP2_SDK;
using TeamPlatform.TP2_SDK.TpResponse;

namespace TP2_App
{
    public partial class frmFileList : Form
    {
        private string m_ApiToken;
        public string ApiToken
        {
            get { return m_ApiToken; }
            set { m_ApiToken = value; }
        }
        
        public frmFileList()
        {
            InitializeComponent();
            setListColumn();
            //setSampleItem();
            getFileList();
        }

        private void getFileList()
        {
            File TpFile = new File();
            List<FileResponse> files = TpFile.all();

            foreach(FileResponse file in files)
            {
                ListViewItem item = new ListViewItem(file.id.ToString());
                item.SubItems.Add(file.filename);
                lvFileList.Items.Add(item);
            }
        }

        private void setListColumn()
        {
            lvFileList.View = View.Details;
            lvFileList.BeginUpdate();
        }

        private void setSampleItem()
        {
            for (int i = 0; i < 10; i++)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add("Test.zip");
                lvFileList.Items.Add(item);
            }
        }
    }
}
