namespace TP2_App
{
    partial class frmTarget
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
            this.cbFolderList = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbFolderList
            // 
            this.cbFolderList.FormattingEnabled = true;
            this.cbFolderList.Location = new System.Drawing.Point(12, 15);
            this.cbFolderList.Name = "cbFolderList";
            this.cbFolderList.Size = new System.Drawing.Size(328, 20);
            this.cbFolderList.TabIndex = 0;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(358, 12);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Ok";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // frmTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 52);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbFolderList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTarget";
            this.Text = "Select folder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFolderList;
        private System.Windows.Forms.Button btnConfirm;
    }
}