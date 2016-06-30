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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTerminal));
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
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
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.GravityChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTableSimulatedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dynamicDataDataSet5BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dynamicDataDataSet5 = new SerialPortTerminal.DynamicDataDataSet5();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStripForm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox17 = new System.Windows.Forms.Label();
            this.data_Table_SimulatedTableAdapter2 = new SerialPortTerminal.DynamicDataDataSet5TableAdapters.Data_Table_SimulatedTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.GravityChart)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableSimulatedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet5BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.Location = new System.Drawing.Point(590, 72);
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
            this.lnkAbout.Location = new System.Drawing.Point(931, 39);
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
            this.label17.Location = new System.Drawing.Point(973, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 13);
            this.label17.TabIndex = 48;
            this.label17.Text = "Latitude";
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
            this.button3.Location = new System.Drawing.Point(419, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(16, 23);
            this.button3.TabIndex = 58;
            this.button3.Text = "200Hz + parameters";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(441, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(16, 23);
            this.button4.TabIndex = 59;
            this.button4.Text = "relays, control sw";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(463, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(16, 23);
            this.button5.TabIndex = 60;
            this.button5.Text = "Wait 20 sec";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(485, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(16, 23);
            this.button6.TabIndex = 61;
            this.button6.Text = "Next - 5 sec";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(507, 46);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(16, 23);
            this.button7.TabIndex = 62;
            this.button7.Text = "20 sec";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(529, 46);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(14, 23);
            this.button8.TabIndex = 63;
            this.button8.Text = "RelaySW = 0x81";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.button13.Location = new System.Drawing.Point(323, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(82, 23);
            this.button13.TabIndex = 68;
            this.button13.Text = "Data Form";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.Location = new System.Drawing.Point(1, 42);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(404, 79);
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
            // GravityChart
            // 
            this.GravityChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GravityChart.BackColor = System.Drawing.Color.Gray;
            this.GravityChart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.GravityChart.BackSecondaryColor = System.Drawing.Color.White;
            this.GravityChart.BorderlineColor = System.Drawing.Color.MidnightBlue;
            this.GravityChart.BorderlineWidth = 5;
            chartArea1.BackColor = System.Drawing.Color.DimGray;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.Name = "Gravity";
            chartArea2.BackColor = System.Drawing.Color.DimGray;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.Name = "Cross Coupling";
            this.GravityChart.ChartAreas.Add(chartArea1);
            this.GravityChart.ChartAreas.Add(chartArea2);
            this.GravityChart.ContextMenuStrip = this.contextMenuStrip1;
            this.GravityChart.DataSource = this.dataTableSimulatedBindingSource;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.White;
            legend1.DockedToChartArea = "Gravity";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Gravity Legend";
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.DockedToChartArea = "Cross Coupling";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Cross Coupling Legend";
            this.GravityChart.Legends.Add(legend1);
            this.GravityChart.Legends.Add(legend2);
            this.GravityChart.Location = new System.Drawing.Point(1, 211);
            this.GravityChart.Name = "GravityChart";
            this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "Gravity";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Gravity Legend";
            series1.Name = "Raw Beam";
            series1.XValueMember = "date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "RawBeam";
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "Gravity";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Gravity Legend";
            series2.Name = "Digital Gravity";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "DigitalGravity";
            series3.ChartArea = "Cross Coupling";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Cross Coupling Legend";
            series3.Name = "XACC2";
            series3.XValueMember = "date";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YValueMembers = "XACC2";
            series4.ChartArea = "Cross Coupling";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Cross Coupling Legend";
            series4.Name = "LACC2";
            series4.XValueMember = "date";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series4.YValueMembers = "LACC2";
            series5.ChartArea = "Cross Coupling";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Cross Coupling Legend";
            series5.Name = "XACC";
            series5.XValueMember = "date";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series5.YValueMembers = "XACC";
            series6.ChartArea = "Cross Coupling";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Cross Coupling Legend";
            series6.Name = "LACC";
            series6.XValueMember = "date";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series6.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series6.YValueMembers = "LACC";
            this.GravityChart.Series.Add(series1);
            this.GravityChart.Series.Add(series2);
            this.GravityChart.Series.Add(series3);
            this.GravityChart.Series.Add(series4);
            this.GravityChart.Series.Add(series5);
            this.GravityChart.Series.Add(series6);
            this.GravityChart.Size = new System.Drawing.Size(1129, 338);
            this.GravityChart.TabIndex = 78;
            this.GravityChart.Text = "chart1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem1.Text = "Background Color";
            // 
            // dataTableSimulatedBindingSource
            // 
            this.dataTableSimulatedBindingSource.DataMember = "Data_Table_Simulated";
            this.dataTableSimulatedBindingSource.DataSource = this.dynamicDataDataSet5BindingSource;
            // 
            // dynamicDataDataSet5
            // 
            this.dynamicDataDataSet5.DataSetName = "DynamicDataDataSet5";
            this.dynamicDataDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 555);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1129, 79);
            this.dataGridView1.TabIndex = 86;
            // 
            // contextMenuStripForm
            // 
            this.contextMenuStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Exit});
            this.contextMenuStripForm.Name = "contextMenuStripForm";
            this.contextMenuStripForm.Size = new System.Drawing.Size(93, 26);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(92, 22);
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // textBox17
            // 
            this.textBox17.AutoSize = true;
            this.textBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox17.Location = new System.Drawing.Point(1036, 4);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(31, 20);
            this.textBox17.TabIndex = 87;
            this.textBox17.Text = "0.0";
            // 
            // data_Table_SimulatedTableAdapter2
            // 
            this.data_Table_SimulatedTableAdapter2.ClearBeforeFill = true;
            // 
            // frmTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 633);
            this.ContextMenuStrip = this.contextMenuStripForm;
            this.Controls.Add(this.textBox17);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.rtfTerminal);
            this.Controls.Add(this.GravityChart);
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
            ((System.ComponentModel.ISupportInitialize)(this.GravityChart)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTableSimulatedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStripForm.ResumeLayout(false);
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

        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox textBox16;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart GravityChart;
        public System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.BindingSource dynamicDataDataSet5BindingSource;
        private DynamicDataDataSet5 dynamicDataDataSet5;
        private System.Windows.Forms.BindingSource dataTableSimulatedBindingSource;
        private DynamicDataDataSet5TableAdapters.Data_Table_SimulatedTableAdapter data_Table_SimulatedTableAdapter2;
        protected System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForm;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.Label textBox17;
    }
}

