namespace SerialPortTerminal
{
    partial class SerialPortForm
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
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbDataPortName = new System.Windows.Forms.ComboBox();
            this.lblDataComPort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.gbPortSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.Controls.Add(this.button3);
            this.gbPortSettings.Controls.Add(this.cmbPortName);
            this.gbPortSettings.Controls.Add(this.lblComPort);
            this.gbPortSettings.Location = new System.Drawing.Point(12, 12);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Size = new System.Drawing.Size(226, 168);
            this.gbPortSettings.TabIndex = 76;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "Meter Serial Port Settings";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbPortName.Location = new System.Drawing.Point(84, 26);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(67, 21);
            this.cmbPortName.TabIndex = 1;
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(6, 29);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(56, 13);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 82;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbDataPortName
            // 
            this.cmbDataPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataPortName.FormattingEnabled = true;
            this.cmbDataPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbDataPortName.Location = new System.Drawing.Point(83, 23);
            this.cmbDataPortName.Name = "cmbDataPortName";
            this.cmbDataPortName.Size = new System.Drawing.Size(67, 21);
            this.cmbDataPortName.TabIndex = 1;
            // 
            // lblDataComPort
            // 
            this.lblDataComPort.AutoSize = true;
            this.lblDataComPort.Location = new System.Drawing.Point(12, 26);
            this.lblDataComPort.Name = "lblDataComPort";
            this.lblDataComPort.Size = new System.Drawing.Size(56, 13);
            this.lblDataComPort.TabIndex = 0;
            this.lblDataComPort.Text = "COM Port:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.lblDataComPort);
            this.groupBox1.Controls.Add(this.cmbDataPortName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(315, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 216);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Serial Port Settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 17);
            this.checkBox1.TabIndex = 78;
            this.checkBox1.Text = "Enable Data Output";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Standard Long Format",
            "Standard ShortFormat",
            "Norstar Format"});
            this.comboBox1.Location = new System.Drawing.Point(257, 39);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 79;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Data Format";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(257, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 81;
            this.button2.Text = "Test Connection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 23);
            this.button3.TabIndex = 84;
            this.button3.Text = "Test Connection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Data Output Interval (Sec)";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(257, 114);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 17);
            this.radioButton1.TabIndex = 84;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1 Second";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(257, 137);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(77, 17);
            this.radioButton2.TabIndex = 85;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "10 Second";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Data Delimitor";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Comma",
            "Space",
            "Tab"});
            this.comboBox2.Location = new System.Drawing.Point(15, 114);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 86;
            // 
            // SerialPortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 320);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbPortSettings);
            this.Name = "SerialPortForm";
            this.Text = "SerialPortForm";
            this.Load += new System.EventHandler(this.SerialPortForm_Load);
            this.gbPortSettings.ResumeLayout(false);
            this.gbPortSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblComPort;
        public System.Windows.Forms.ComboBox cmbPortName;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.GroupBox gbPortSettings;
        public System.Windows.Forms.ComboBox cmbDataPortName;
        private System.Windows.Forms.Label lblDataComPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}