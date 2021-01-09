namespace Child
{
    partial class Form1
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
            this.LoginBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ChildIDTxt = new System.Windows.Forms.TextBox();
            this.ParentIDTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.StatusLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(220, 282);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(126, 39);
            this.LoginBtn.TabIndex = 0;
            this.LoginBtn.Text = "Start!!";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parent ID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Child ID :";
            // 
            // ChildIDTxt
            // 
            this.ChildIDTxt.Location = new System.Drawing.Point(167, 223);
            this.ChildIDTxt.Name = "ChildIDTxt";
            this.ChildIDTxt.Size = new System.Drawing.Size(268, 22);
            this.ChildIDTxt.TabIndex = 3;
            // 
            // ParentIDTxt
            // 
            this.ParentIDTxt.Location = new System.Drawing.Point(167, 173);
            this.ParentIDTxt.Name = "ParentIDTxt";
            this.ParentIDTxt.Size = new System.Drawing.Size(268, 22);
            this.ParentIDTxt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(444, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Please Enter Parent ID That Registered in WebSite \"95.211.250.104\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "And Enter Child ID To Register";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(405, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "This page is shown only when the child has not yet registered !!";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(31, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "CAUTION :";
            // 
            // StatusLab
            // 
            this.StatusLab.AutoSize = true;
            this.StatusLab.ForeColor = System.Drawing.Color.Red;
            this.StatusLab.Location = new System.Drawing.Point(203, 133);
            this.StatusLab.Name = "StatusLab";
            this.StatusLab.Size = new System.Drawing.Size(0, 17);
            this.StatusLab.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 358);
            this.Controls.Add(this.StatusLab);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ParentIDTxt);
            this.Controls.Add(this.ChildIDTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginBtn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Setting Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ChildIDTxt;
        private System.Windows.Forms.TextBox ParentIDTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label StatusLab;
    }
}

