namespace TP2_App
{
    partial class frmFileList
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
            this.SuspendLayout();
            // 
            // lvFileList
            // 
            this.lvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chFilename});
            this.lvFileList.Location = new System.Drawing.Point(12, 12);
            this.lvFileList.Name = "lvFileList";
            this.lvFileList.Size = new System.Drawing.Size(811, 471);
            this.lvFileList.TabIndex = 0;
            this.lvFileList.UseCompatibleStateImageBehavior = false;
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
            // frmFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 495);
            this.Controls.Add(this.lvFileList);
            this.Name = "frmFileList";
            this.Text = "File List";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFileList;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chFilename;
    }
}