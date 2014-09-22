namespace TP2_App
{
    partial class frmModelList
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.dlgUploadModel = new System.Windows.Forms.OpenFileDialog();
            this.panList = new System.Windows.Forms.Panel();
            this.panList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFileList
            // 
            this.lvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chFilename});
            this.lvFileList.Location = new System.Drawing.Point(3, 3);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(671, 489);
            this.lvFileList.TabIndex = 0;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
            this.lvFileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFileList_MouseDoubleClick);
            // 
            // chId
            // 
            this.chId.Text = "ID";
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 200;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(680, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(143, 30);
            this.btnUpload.TabIndex = 1;
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
            this.panList.Controls.Add(this.btnUpload);
            this.panList.Controls.Add(this.lvFileList);
            this.panList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panList.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panList.Location = new System.Drawing.Point(0, 0);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(835, 495);
            this.panList.TabIndex = 3;
            // 
            // frmModelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 495);
            this.Controls.Add(this.panList);
            this.Name = "frmModelList";
            this.Text = "File List";
            this.panList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog dlgUploadModel;
        private System.Windows.Forms.Panel panList;
    }
}