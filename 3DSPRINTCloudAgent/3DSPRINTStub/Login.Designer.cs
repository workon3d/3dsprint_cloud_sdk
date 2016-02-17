namespace _3DSPRINTStub
{
    partial class Login
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
            this.signinBtn = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // signinBtn
            // 
            this.signinBtn.Location = new System.Drawing.Point(216, 21);
            this.signinBtn.Name = "signinBtn";
            this.signinBtn.Size = new System.Drawing.Size(75, 48);
            this.signinBtn.TabIndex = 2;
            this.signinBtn.Text = "Sign-In";
            this.signinBtn.UseVisualStyleBackColor = true;
            this.signinBtn.Click += new System.EventHandler(this.signinBtn_Click);
            // 
            // password
            // 
            this.password.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::_3DSPRINTStub.Properties.Settings.Default, "password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.password.Location = new System.Drawing.Point(12, 48);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(187, 21);
            this.password.TabIndex = 1;
            this.password.Tag = "password";
            this.password.Text = global::_3DSPRINTStub.Properties.Settings.Default.password;
            this.password.UseSystemPasswordChar = true;
            // 
            // email
            // 
            this.email.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::_3DSPRINTStub.Properties.Settings.Default, "email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.email.Location = new System.Drawing.Point(12, 21);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(187, 21);
            this.email.TabIndex = 0;
            this.email.Tag = "email";
            this.email.Text = global::_3DSPRINTStub.Properties.Settings.Default.email;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(306, 21);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 48);
            this.save.TabIndex = 3;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 96);
            this.Controls.Add(this.save);
            this.Controls.Add(this.signinBtn);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Name = "Login";
            this.Text = "3DSPRINT Stub";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button signinBtn;
        private System.Windows.Forms.Button save;
    }
}

