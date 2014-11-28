namespace TSCloud_SampleApp
{
    partial class frmCreateFolder
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbModelList = new System.Windows.Forms.ComboBox();
            this.tbModelName = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Folder name";
            // 
            // cbModelList
            // 
            this.cbModelList.FormattingEnabled = true;
            this.cbModelList.Location = new System.Drawing.Point(118, 34);
            this.cbModelList.Name = "cbModelList";
            this.cbModelList.Size = new System.Drawing.Size(297, 20);
            this.cbModelList.TabIndex = 2;
            // 
            // tbModelName
            // 
            this.tbModelName.Location = new System.Drawing.Point(118, 67);
            this.tbModelName.Name = "tbModelName";
            this.tbModelName.Size = new System.Drawing.Size(297, 21);
            this.tbModelName.TabIndex = 3;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(169, 122);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(97, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent";
            // 
            // frmCreateFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 170);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.tbModelName);
            this.Controls.Add(this.cbModelList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateFolder";
            this.Text = "Folder create";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbModelList;
        private System.Windows.Forms.TextBox tbModelName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
    }
}