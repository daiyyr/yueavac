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
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginB
            // 
            this.loginB.Location = new System.Drawing.Point(1191, 20);
            this.loginB.Margin = new System.Windows.Forms.Padding(4);
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(84, 43);
            this.loginB.TabIndex = 1;
            this.loginB.Text = "forceLogin";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Visible = false;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // autoB
            // 
            this.autoB.Location = new System.Drawing.Point(535, 19);
            this.autoB.Margin = new System.Windows.Forms.Padding(4);
            this.autoB.Name = "autoB";
            this.autoB.Size = new System.Drawing.Size(104, 33);
            this.autoB.TabIndex = 0;
            this.autoB.Text = "start";
            this.autoB.UseVisualStyleBackColor = true;
            this.autoB.Click += new System.EventHandler(this.autoB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "rate:";
            // 
            // rate
            // 
            this.rate.Location = new System.Drawing.Point(68, 20);
            this.rate.Margin = new System.Windows.Forms.Padding(4);
            this.rate.Name = "rate";
            this.rate.Size = new System.Drawing.Size(41, 22);
            this.rate.TabIndex = 4;
            this.rate.Text = "500";
            this.rate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(179, 569);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "copyright Mushroom";
            // 
            // logT
            // 
            this.logT.Location = new System.Drawing.Point(11, 57);
            this.logT.Margin = new System.Windows.Forms.Padding(4);
            this.logT.Name = "logT";
            this.logT.ReadOnly = true;
            this.logT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logT.Size = new System.Drawing.Size(628, 368);
            this.logT.TabIndex = 9;
            this.logT.Text = "";
            this.logT.TextChanged += new System.EventHandler(this.logT_TextChanged);
            // 
            // addB
            // 
            this.addB.Location = new System.Drawing.Point(997, 20);
            this.addB.Margin = new System.Windows.Forms.Padding(4);
            this.addB.Name = "addB";
            this.addB.Size = new System.Drawing.Size(93, 31);
            this.addB.TabIndex = 11;
            this.addB.Text = "add";
            this.addB.UseVisualStyleBackColor = true;
            this.addB.Visible = false;
            this.addB.Click += new System.EventHandler(this.addB_Click);
            // 
            // deleteB
            // 
            this.deleteB.Location = new System.Drawing.Point(1099, 20);
            this.deleteB.Margin = new System.Windows.Forms.Padding(4);
            this.deleteB.Name = "deleteB";
            this.deleteB.Size = new System.Drawing.Size(84, 31);
            this.deleteB.TabIndex = 12;
            this.deleteB.Text = "delete";
            this.deleteB.UseVisualStyleBackColor = true;
            this.deleteB.Visible = false;
            this.deleteB.Click += new System.EventHandler(this.deleteB_Click);
            // 
            // inputT
            // 
            this.inputT.Location = new System.Drawing.Point(813, 24);
            this.inputT.Margin = new System.Windows.Forms.Padding(4);
            this.inputT.Name = "inputT";
            this.inputT.Size = new System.Drawing.Size(352, 22);
            this.inputT.TabIndex = 13;
            this.inputT.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(777, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "url:";
            this.label4.Visible = false;
            // 
            // urlList
            // 
            this.urlList.FormattingEnabled = true;
            this.urlList.Location = new System.Drawing.Point(855, 57);
            this.urlList.Margin = new System.Windows.Forms.Padding(4);
            this.urlList.Name = "urlList";
            this.urlList.Size = new System.Drawing.Size(423, 55);
            this.urlList.TabIndex = 15;
            this.urlList.Visible = false;
            // 
            // testLog
            // 
            this.testLog.Location = new System.Drawing.Point(813, 147);
            this.testLog.Margin = new System.Windows.Forms.Padding(4);
            this.testLog.Name = "testLog";
            this.testLog.ReadOnly = true;
            this.testLog.Size = new System.Drawing.Size(716, 417);
            this.testLog.TabIndex = 16;
            this.testLog.Text = "";
            this.testLog.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(11, 435);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 83);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(280, 476);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(152, 22);
            this.textBox1.TabIndex = 18;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 444);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "输入5个字符后将直接提交";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 559);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 31);
            this.button1.TabIndex = 20;
            this.button1.Text = "重输验证码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 18F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(5, 521);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 30);
            this.label6.TabIndex = 21;
            this.label6.Text = "请输入验证码";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(532, 559);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "若输入有误请点击下面的按钮";
            this.label7.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(755, 94);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 69);
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
            this.comboBox1.Location = new System.Drawing.Point(194, 24);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 24);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(147, 29);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "type:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(364, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "mode";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "快速",
            "安全"});
            this.comboBox2.Location = new System.Drawing.Point(414, 24);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(101, 24);
            this.comboBox2.TabIndex = 24;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 589);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox2);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

