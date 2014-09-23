namespace TP2_App
{
    partial class frmModelView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbId = new System.Windows.Forms.TextBox();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelFtype = new System.Windows.Forms.Label();
            this.labelMeta = new System.Windows.Forms.Label();
            this.labelAcl = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbFtype = new System.Windows.Forms.TextBox();
            this.tbMeta = new System.Windows.Forms.TextBox();
            this.tbAcl = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.dlgUploadModel = new System.Windows.Forms.OpenFileDialog();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dlgDownloadModel = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.tbId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelFilename, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelKey, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelFtype, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelMeta, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelAcl, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbFilename, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbKey, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFtype, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbMeta, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbAcl, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelId, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 15);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(349, 508);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(90, 4);
            this.tbId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(256, 23);
            this.tbId.TabIndex = 6;
            this.tbId.TextChanged += new System.EventHandler(this.tbId_TextChanged);
            // 
            // labelFilename
            // 
            this.labelFilename.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelFilename.AutoSize = true;
            this.labelFilename.Location = new System.Drawing.Point(22, 39);
            this.labelFilename.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(55, 15);
            this.labelFilename.TabIndex = 1;
            this.labelFilename.Text = "Filename";
            this.labelFilename.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKey
            // 
            this.labelKey.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(51, 70);
            this.labelKey.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(26, 15);
            this.labelKey.TabIndex = 2;
            this.labelKey.Text = "Key";
            this.labelKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFtype
            // 
            this.labelFtype.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelFtype.AutoSize = true;
            this.labelFtype.Location = new System.Drawing.Point(41, 101);
            this.labelFtype.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelFtype.Name = "labelFtype";
            this.labelFtype.Size = new System.Drawing.Size(36, 15);
            this.labelFtype.TabIndex = 3;
            this.labelFtype.Text = "Ftype";
            this.labelFtype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMeta
            // 
            this.labelMeta.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMeta.AutoSize = true;
            this.labelMeta.Location = new System.Drawing.Point(43, 212);
            this.labelMeta.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelMeta.Name = "labelMeta";
            this.labelMeta.Size = new System.Drawing.Size(34, 15);
            this.labelMeta.TabIndex = 4;
            this.labelMeta.Text = "Meta";
            this.labelMeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAcl
            // 
            this.labelAcl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelAcl.AutoSize = true;
            this.labelAcl.Location = new System.Drawing.Point(53, 404);
            this.labelAcl.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelAcl.Name = "labelAcl";
            this.labelAcl.Size = new System.Drawing.Size(24, 15);
            this.labelAcl.TabIndex = 5;
            this.labelAcl.Text = "Acl";
            this.labelAcl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFilename
            // 
            this.tbFilename.Location = new System.Drawing.Point(90, 35);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(256, 23);
            this.tbFilename.TabIndex = 7;
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(90, 66);
            this.tbKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbKey.Name = "tbKey";
            this.tbKey.ReadOnly = true;
            this.tbKey.Size = new System.Drawing.Size(256, 23);
            this.tbKey.TabIndex = 8;
            this.tbKey.TextChanged += new System.EventHandler(this.tbKey_TextChanged);
            // 
            // tbFtype
            // 
            this.tbFtype.Location = new System.Drawing.Point(90, 97);
            this.tbFtype.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFtype.Name = "tbFtype";
            this.tbFtype.ReadOnly = true;
            this.tbFtype.Size = new System.Drawing.Size(256, 23);
            this.tbFtype.TabIndex = 9;
            this.tbFtype.TextChanged += new System.EventHandler(this.tbFtype_TextChanged);
            // 
            // tbMeta
            // 
            this.tbMeta.Location = new System.Drawing.Point(90, 128);
            this.tbMeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMeta.Multiline = true;
            this.tbMeta.Name = "tbMeta";
            this.tbMeta.Size = new System.Drawing.Size(256, 178);
            this.tbMeta.TabIndex = 10;
            this.tbMeta.TextChanged += new System.EventHandler(this.tbMeta_TextChanged);
            // 
            // tbAcl
            // 
            this.tbAcl.Location = new System.Drawing.Point(90, 320);
            this.tbAcl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbAcl.Multiline = true;
            this.tbAcl.Name = "tbAcl";
            this.tbAcl.Size = new System.Drawing.Size(256, 178);
            this.tbAcl.TabIndex = 11;
            this.tbAcl.TextChanged += new System.EventHandler(this.tbAcl_TextChanged);
            // 
            // labelId
            // 
            this.labelId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(60, 8);
            this.labelId.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(17, 15);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "Id";
            this.labelId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(368, 16);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(129, 29);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(368, 139);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(129, 29);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(368, 50);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(129, 29);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "File Change";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // dlgUploadModel
            // 
            this.dlgUploadModel.FileName = "openDlg";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(368, 102);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(128, 29);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Save as...";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // frmModelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 545);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModelView";
            this.ShowInTaskbar = false;
            this.Text = "ModelView";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModelView_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelFtype;
        private System.Windows.Forms.Label labelMeta;
        private System.Windows.Forms.Label labelAcl;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbFtype;
        private System.Windows.Forms.TextBox tbMeta;
        private System.Windows.Forms.TextBox tbAcl;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog dlgUploadModel;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.SaveFileDialog dlgDownloadModel;
    }
}