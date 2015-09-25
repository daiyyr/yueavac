namespace WHA_avac
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.loginB = new System.Windows.Forms.Button();
            this.autoB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logT = new System.Windows.Forms.RichTextBox();
            this.addB = new System.Windows.Forms.Button();
            this.deleteB = new System.Windows.Forms.Button();
            this.inputT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.urlList = new System.Windows.Forms.CheckedListBox();
            this.testLog = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginB
            // 
            this.loginB.Location = new System.Drawing.Point(893, 15);
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(63, 32);
            this.loginB.TabIndex = 1;
            this.loginB.Text = "forceLogin";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Visible = false;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // autoB
            // 
            this.autoB.Location = new System.Drawing.Point(285, 12);
            this.autoB.Name = "autoB";
            this.autoB.Size = new System.Drawing.Size(94, 25);
            this.autoB.TabIndex = 0;
            this.autoB.Text = "start";
            this.autoB.UseVisualStyleBackColor = true;
            this.autoB.Click += new System.EventHandler(this.autoB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "rate:";
            // 
            // rate
            // 
            this.rate.Location = new System.Drawing.Point(51, 15);
            this.rate.Name = "rate";
            this.rate.Size = new System.Drawing.Size(32, 21);
            this.rate.TabIndex = 4;
            this.rate.Text = "500";
            this.rate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(134, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "copyright Mushroom";
            // 
            // logT
            // 
            this.logT.Location = new System.Drawing.Point(8, 43);
            this.logT.Name = "logT";
            this.logT.ReadOnly = true;
            this.logT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logT.Size = new System.Drawing.Size(373, 175);
            this.logT.TabIndex = 9;
            this.logT.Text = "";
            this.logT.TextChanged += new System.EventHandler(this.logT_TextChanged);
            // 
            // addB
            // 
            this.addB.Location = new System.Drawing.Point(748, 15);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(70, 23);
            this.addB.TabIndex = 11;
            this.addB.Text = "add";
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Visible = false;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // deleteB
            // 
            this.deleteB.Location = new System.Drawing.Point(824, 15);
            this.deleteB.Name = "deleteB";
            this.deleteB.Size = new System.Drawing.Size(63, 23);
            this.deleteB.TabIndex = 12;
            this.deleteB.Text = "delete";
            this.deleteB.UseVisualStyleBackColor = true;
            this.deleteB.Visible = false;
            this.deleteB.Click += new System.EventHandler(this.deleteB_Click);
            // 
            // inputT
            // 
            this.inputT.Location = new System.Drawing.Point(460, 17);
            this.inputT.Name = "inputT";
            this.inputT.Size = new System.Drawing.Size(265, 21);
            this.inputT.TabIndex = 13;
            this.inputT.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "url:";
            this.label4.Visible = false;
            // 
            // urlList
            // 
            this.urlList.FormattingEnabled = true;
            this.urlList.Location = new System.Drawing.Point(553, 44);
            this.urlList.Name = "urlList";
            this.urlList.Size = new System.Drawing.Size(318, 52);
            this.urlList.TabIndex = 15;
            this.urlList.Visible = false;
            // 
            // testLog
            // 
            this.testLog.Location = new System.Drawing.Point(401, 102);
            this.testLog.Name = "testLog";
            this.testLog.ReadOnly = true;
            this.testLog.Size = new System.Drawing.Size(538, 314);
            this.testLog.TabIndex = 16;
            this.testLog.Text = "";
            this.testLog.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 224);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 62);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(208, 275);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 21);
            this.textBox1.TabIndex = 18;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "输入5个字符后将直接提交";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(306, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "重输验证码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 20F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 27);
            this.label6.TabIndex = 21;
            this.label6.Text = "请输入验证码";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "若输入有误请点击下面的按钮";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 52);
            this.button2.TabIndex = 23;
            this.button2.Text = "test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Work and Holiday",
            "General"});
            this.comboBox1.Location = new System.Drawing.Point(146, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "type:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 340);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.testLog);
            this.Controls.Add(this.urlList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputT);
            this.Controls.Add(this.deleteB);
            this.Controls.Add(this.addB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logT);
            this.Controls.Add(this.autoB);
            this.Controls.Add(this.loginB);
            this.Name = "Form1";
            this.Text = "avac单用户版";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loginB;
        private System.Windows.Forms.Button autoB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox logT;
        private System.Windows.Forms.Button addB;
        private System.Windows.Forms.Button deleteB;
        private System.Windows.Forms.TextBox inputT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox urlList;
        private System.Windows.Forms.RichTextBox testLog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
    }
}

