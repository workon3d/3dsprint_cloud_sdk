namespace TSCloud_SampleApp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvFileList = new System.Windows.Forms.ListView();
            this.chId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMeta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnUpload = new System.Windows.Forms.Button();
            this.dlgUploadModel = new System.Windows.Forms.OpenFileDialog();
            this.panList = new System.Windows.Forms.Panel();
            this.btnCreateFolder = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvQueueList = new System.Windows.Forms.ListView();
            this.chQueueId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQueueMeta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPrinterList = new System.Windows.Forms.ListView();
            this.chPrinterId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPrinterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panList.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFileList
            // 
            this.lvFileList.AllowDrop = true;
            this.lvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chFilename,
            this.chMeta});
            this.lvFileList.Location = new System.Drawing.Point(5, 33);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(1017, 519);
            this.lvFileList.TabIndex = 0;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            this.lvFileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvFileList_KeyDown);
            this.lvFileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFileList_MouseDoubleClick);
            // 
            // chId
            // 
            this.chId.Text = "ID";
            this.chId.Width = 100;
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 300;
            // 
            // chMeta
            // 
            this.chMeta.Text = "Meta";
            this.chMeta.Width = 425;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(3, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(115, 23);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Upload Model";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // dlgUploadModel
            // 
            this.dlgUploadModel.FileName = "openDlg";
            // 
            // panList
            // 
            this.panList.Controls.Add(this.btnCreateFolder);
            this.panList.Controls.Add(this.btnClear);
            this.panList.Controls.Add(this.tbQuery);
            this.panList.Controls.Add(this.btnSearch);
            this.panList.Controls.Add(this.btnUpload);
            this.panList.Controls.Add(this.lvFileList);
            this.panList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panList.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panList.Location = new System.Drawing.Point(3, 3);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(1028, 555);
            this.panList.TabIndex = 3;
            // 
            // btnCreateFolder
            // 
            this.btnCreateFolder.Location = new System.Drawing.Point(125, 4);
            this.btnCreateFolder.Name = "btnCreateFolder";
            this.btnCreateFolder.Size = new System.Drawing.Size(115, 23);
            this.btnCreateFolder.TabIndex = 5;
            this.btnCreateFolder.Text = "Create folder";
            this.btnCreateFolder.UseVisualStyleBackColor = true;
            this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(952, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(652, 6);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(215, 23);
            this.tbQuery.TabIndex = 1;
            this.tbQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbQuery_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(873, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1042, 587);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1034, 561);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvQueueList);
            this.tabPage2.Controls.Add(this.lvPrinterList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1034, 561);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Printer Queues";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvQueueList
            // 
            this.lvQueueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chQueueId,
            this.chQueueMeta});
            this.lvQueueList.Location = new System.Drawing.Point(224, 6);
            this.lvQueueList.Name = "lvQueueList";
            this.lvQueueList.Size = new System.Drawing.Size(807, 552);
            this.lvQueueList.TabIndex = 1;
            this.lvQueueList.UseCompatibleStateImageBehavior = false;
            // 
            // chQueueId
            // 
            this.chQueueId.Text = "ID";
            // 
            // chQueueMeta
            // 
            this.chQueueMeta.Text = "Meta";
            this.chQueueMeta.Width = 500;
            // 
            // lvPrinterList
            // 
            this.lvPrinterList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPrinterId,
            this.chPrinterName});
            this.lvPrinterList.Location = new System.Drawing.Point(3, 6);
            this.lvPrinterList.Name = "lvPrinterList";
            this.lvPrinterList.Size = new System.Drawing.Size(215, 549);
            this.lvPrinterList.TabIndex = 0;
            this.lvPrinterList.UseCompatibleStateImageBehavior = false;
            this.lvPrinterList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPrinterList_MouseDoubleClick);
            // 
            // chPrinterId
            // 
            this.chPrinterId.Text = "ID";
            // 
            // chPrinterName
            // 
            this.chPrinterName.Text = "Name";
            this.chPrinterName.Width = 180;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 593);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "Model List";
            this.panList.ResumeLayout(false);
            this.panList.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog dlgUploadModel;
        private System.Windows.Forms.Panel panList;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ColumnHeader chMeta;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvQueueList;
        private System.Windows.Forms.ListView lvPrinterList;
        private System.Windows.Forms.ColumnHeader chPrinterId;
        private System.Windows.Forms.ColumnHeader chPrinterName;
        private System.Windows.Forms.ColumnHeader chQueueId;
        private System.Windows.Forms.ColumnHeader chQueueMeta;
    }
}