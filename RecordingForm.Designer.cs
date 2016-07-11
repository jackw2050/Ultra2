namespace SerialPortTerminal
{
    partial class RecordingForm
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
            this.refreshTimeComboBox = new System.Windows.Forms.ComboBox();
            this.dailyComboBox = new System.Windows.Forms.ComboBox();
            this.hourlyComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dailyRefreshPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.dailyRefreshPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimeComboBox
            // 
            this.refreshTimeComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.refreshTimeComboBox.FormattingEnabled = true;
            this.refreshTimeComboBox.Items.AddRange(new object[] {
            "0100",
            "0200",
            "0300",
            "0400",
            "0500",
            "0600",
            "0700",
            "0800",
            "0900",
            "1000",
            "1100",
            "1200",
            "1300",
            "1400",
            "1500",
            "1600",
            "1700",
            "1800",
            "1900",
            "2000",
            "2100",
            "2200",
            "2300",
            "3400"});
            this.refreshTimeComboBox.Location = new System.Drawing.Point(261, 120);
            this.refreshTimeComboBox.Name = "refreshTimeComboBox";
            this.refreshTimeComboBox.Size = new System.Drawing.Size(158, 21);
            this.refreshTimeComboBox.TabIndex = 15;
            this.refreshTimeComboBox.Text = "Specify Time To Refresh File";
            this.refreshTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.refreshTimeComboBox_SelectedIndexChanged);
            // 
            // dailyComboBox
            // 
            this.dailyComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dailyComboBox.FormattingEnabled = true;
            this.dailyComboBox.Items.AddRange(new object[] {
            "24 Hours From Start Time",
            "At Midnight",
            "At Other Time"});
            this.dailyComboBox.Location = new System.Drawing.Point(261, 92);
            this.dailyComboBox.Name = "dailyComboBox";
            this.dailyComboBox.Size = new System.Drawing.Size(158, 21);
            this.dailyComboBox.TabIndex = 14;
            this.dailyComboBox.Text = "Specify File Refresh Rate";
            this.dailyComboBox.SelectedIndexChanged += new System.EventHandler(this.dailyComboBox_SelectedIndexChanged);
            // 
            // hourlyComboBox
            // 
            this.hourlyComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.hourlyComboBox.FormattingEnabled = true;
            this.hourlyComboBox.Items.AddRange(new object[] {
            "From Start Time",
            "On The Hour"});
            this.hourlyComboBox.Location = new System.Drawing.Point(261, 64);
            this.hourlyComboBox.Name = "hourlyComboBox";
            this.hourlyComboBox.Size = new System.Drawing.Size(158, 21);
            this.hourlyComboBox.TabIndex = 13;
            this.hourlyComboBox.Text = "Specify File Refresh Rate";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(18, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 123);
            this.panel1.TabIndex = 12;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Manual";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(13, 62);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(92, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "New File Daily";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(99, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "New File Hourly";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "You can start and stop recording files manually or\r\n have this  software open a n" +
    "ew file a a specified interval.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(344, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(16, 16);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown4.TabIndex = 12;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(16, 42);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown5.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Hours";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Minutes";
            // 
            // dailyRefreshPanel
            // 
            this.dailyRefreshPanel.Controls.Add(this.label7);
            this.dailyRefreshPanel.Controls.Add(this.label6);
            this.dailyRefreshPanel.Controls.Add(this.numericUpDown5);
            this.dailyRefreshPanel.Controls.Add(this.numericUpDown4);
            this.dailyRefreshPanel.Location = new System.Drawing.Point(261, 151);
            this.dailyRefreshPanel.Name = "dailyRefreshPanel";
            this.dailyRefreshPanel.Size = new System.Drawing.Size(136, 74);
            this.dailyRefreshPanel.TabIndex = 17;
            // 
            // RecordingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 358);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dailyRefreshPanel);
            this.Controls.Add(this.refreshTimeComboBox);
            this.Controls.Add(this.dailyComboBox);
            this.Controls.Add(this.hourlyComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "RecordingForm";
            this.Text = "RecordingForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.dailyRefreshPanel.ResumeLayout(false);
            this.dailyRefreshPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox refreshTimeComboBox;
        private System.Windows.Forms.ComboBox dailyComboBox;
        private System.Windows.Forms.ComboBox hourlyComboBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel dailyRefreshPanel;
    }
}