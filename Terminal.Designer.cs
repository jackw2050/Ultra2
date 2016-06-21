namespace SerialPortTerminal
{
  partial class frmTerminal
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTerminal));
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataTableSimulatedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.TorqueMotorButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dynamicDataDataSet2 = new SerialPortTerminal.DynamicDataDataSet2();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableSimulatedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.Location = new System.Drawing.Point(590, 77);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 6;
            this.btnOpenPort.Text = "&Open Port";
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // lnkAbout
            // 
            this.lnkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Location = new System.Drawing.Point(914, 18);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(35, 13);
            this.lnkAbout.TabIndex = 8;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "&About";
            this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
            // 
            // tmrCheckComPorts
            // 
            this.tmrCheckComPorts.Enabled = true;
            this.tmrCheckComPorts.Interval = 500;
            this.tmrCheckComPorts.Tick += new System.EventHandler(this.tmrCheckComPorts_Tick);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(419, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 25);
            this.button2.TabIndex = 11;
            this.button2.Text = "Start data collection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // _timer1
            // 
            this._timer1.Interval = 1000;
            // 
            // dataTableSimulatedBindingSource
            // 
            this.dataTableSimulatedBindingSource.DataSource = this.dynamicDataDataSet2;
            this.dataTableSimulatedBindingSource.Position = 0;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(760, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(118, 20);
            this.textBox5.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(716, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "Data Status Form";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(985, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "Altitude";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(988, 104);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 20);
            this.textBox16.TabIndex = 49;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(985, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 13);
            this.label17.TabIndex = 48;
            this.label17.Text = "Latitude";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(988, 26);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(100, 20);
            this.textBox17.TabIndex = 47;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(985, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 46;
            this.label18.Text = "Longitude";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(988, 65);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(100, 20);
            this.textBox18.TabIndex = 45;
            // 
            // TorqueMotorButton
            // 
            this.TorqueMotorButton.Location = new System.Drawing.Point(1019, 182);
            this.TorqueMotorButton.Name = "TorqueMotorButton";
            this.TorqueMotorButton.Size = new System.Drawing.Size(91, 23);
            this.TorqueMotorButton.TabIndex = 57;
            this.TorqueMotorButton.Text = "Torque Motor";
            this.TorqueMotorButton.UseVisualStyleBackColor = true;
            this.TorqueMotorButton.Click += new System.EventHandler(this.TorqueMotorButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(419, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 23);
            this.button3.TabIndex = 58;
            this.button3.Text = "200Hz + parameters";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(419, 69);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 23);
            this.button4.TabIndex = 59;
            this.button4.Text = "relays, control sw";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(419, 98);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 23);
            this.button5.TabIndex = 60;
            this.button5.Text = "Wait 20 sec";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(419, 127);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(144, 23);
            this.button6.TabIndex = 61;
            this.button6.Text = "Next - 5 sec";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(419, 156);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(144, 23);
            this.button7.TabIndex = 62;
            this.button7.Text = "20 sec";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(419, 182);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(144, 23);
            this.button8.TabIndex = 63;
            this.button8.Text = "RelaySW = 0x81";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(590, 43);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(127, 23);
            this.button9.TabIndex = 64;
            this.button9.Text = "Torque Motor ON";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(723, 43);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(127, 23);
            this.button10.TabIndex = 65;
            this.button10.Text = "Torque Motor OFF";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(590, 14);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(105, 23);
            this.button11.TabIndex = 66;
            this.button11.Text = "Auto Start";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(108, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(107, 23);
            this.button12.TabIndex = 67;
            this.button12.Text = "Status Form";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(983, 153);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(117, 23);
            this.button13.TabIndex = 68;
            this.button13.Text = "Enable Torque Motor";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.dataTableSimulatedBindingSource;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(1, 237);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1087, 291);
            this.chart1.TabIndex = 78;
            this.chart1.Text = "chart1";
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.Location = new System.Drawing.Point(1, 42);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(387, 58);
            this.rtfTerminal.TabIndex = 84;
            this.rtfTerminal.Text = "";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1, 3);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(104, 23);
            this.button14.TabIndex = 85;
            this.button14.Text = "Serial Port Form";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.dataTableSimulatedBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(1, 534);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(898, 79);
            this.dataGridView1.TabIndex = 86;
            // 
            // dynamicDataDataSet2
            // 
            this.dynamicDataDataSet2.DataSetName = "DynamicDataDataSet2";
            this.dynamicDataDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 612);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.rtfTerminal);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TorqueMotorButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBox18);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.btnOpenPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(505, 250);
            this.Name = "frmTerminal";
            this.Text = "ZLS Corporation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTerminal_FormClosing);
            this.Load += new System.EventHandler(this.frmTerminal_Load);
            this.Shown += new System.EventHandler(this.frmTerminal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataTableSimulatedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btnOpenPort;
    private System.Windows.Forms.LinkLabel lnkAbout;
		private System.Windows.Forms.Timer tmrCheckComPorts;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer _timer1;
        private DynamicDataDataSet dynamicDataDataSet;
        private System.Windows.Forms.BindingSource dataTableSimulatedBindingSource;
        private DynamicDataDataSetTableAdapters.Data_Table_SimulatedTableAdapter data_Table_SimulatedTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn springTensionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawBeamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn digitalGravityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossCouplingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yEARDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dAYSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hOURDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mINDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sECDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vCCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aX2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xACC2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lACC2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xACCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lACCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn altitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPSStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox textBox16;
        public System.Windows.Forms.TextBox textBox17;
        public System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Button TorqueMotorButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        public System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DynamicDataDataSet2 dynamicDataDataSet2;
    }
}

