namespace TSCloud_SampleApp
{
    partial class frmSignIn
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
            this.btnSignIn = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSignIn
            // 
            this.btnSignIn.Location = new System.Drawing.Point(117, 116);
            this.btnSignIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(146, 29);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // email
            // 
            this.email.AutoSize = true;
            this.email.Location = new System.Drawing.Point(37, 35);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(41, 15);
            this.email.TabIndex = 1;
            this.email.Text = "E-mail";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(37, 71);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(57, 15);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(117, 31);
            this.tbEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(235, 23);
            this.tbEmail.TabIndex = 0;
            this.tbEmail.Text = "inseok.lee@3dsystems.com";
            this.tbEmail.TextChanged += new System.EventHandler(this.tbEmail_TextChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(117, 71);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(235, 23);
            this.tbPassword.TabIndex = 4;
            this.tbPassword.Text = "dldlstjr";
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            // 
            // frmSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 172);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.email);
            this.Controls.Add(this.btnSignIn);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmSignIn";
            this.Text = "3DSPRINT Cloud SDK Sample App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.TextBox tbPassword;
    }
}

