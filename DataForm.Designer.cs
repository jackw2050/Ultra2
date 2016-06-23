namespace SerialPortTerminal
{
    partial class DataForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.meterNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beamScaleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engPasswordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crossPeriodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longPeriodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crossDampFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longDampFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crossGainDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longGainDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crossLeadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longLeadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.springTensionMaxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crossBiasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longBiasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dynamicDataDataSet5 = new SerialPortTerminal.DynamicDataDataSet5();
            this.configDataTableAdapter = new SerialPortTerminal.DynamicDataDataSet5TableAdapters.ConfigDataTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet5)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.meterNumberDataGridViewTextBoxColumn,
            this.beamScaleDataGridViewTextBoxColumn,
            this.engPasswordDataGridViewTextBoxColumn,
            this.crossPeriodDataGridViewTextBoxColumn,
            this.longPeriodDataGridViewTextBoxColumn,
            this.crossDampFactorDataGridViewTextBoxColumn,
            this.longDampFactorDataGridViewTextBoxColumn,
            this.crossGainDataGridViewTextBoxColumn,
            this.longGainDataGridViewTextBoxColumn,
            this.crossLeadDataGridViewTextBoxColumn,
            this.longLeadDataGridViewTextBoxColumn,
            this.springTensionMaxDataGridViewTextBoxColumn,
            this.crossBiasDataGridViewTextBoxColumn,
            this.longBiasDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.configDataBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 378);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(812, 70);
            this.dataGridView1.TabIndex = 0;
            // 
            // meterNumberDataGridViewTextBoxColumn
            // 
            this.meterNumberDataGridViewTextBoxColumn.DataPropertyName = "meterNumber";
            this.meterNumberDataGridViewTextBoxColumn.HeaderText = "meterNumber";
            this.meterNumberDataGridViewTextBoxColumn.Name = "meterNumberDataGridViewTextBoxColumn";
            // 
            // beamScaleDataGridViewTextBoxColumn
            // 
            this.beamScaleDataGridViewTextBoxColumn.DataPropertyName = "beamScale";
            this.beamScaleDataGridViewTextBoxColumn.HeaderText = "beamScale";
            this.beamScaleDataGridViewTextBoxColumn.Name = "beamScaleDataGridViewTextBoxColumn";
            // 
            // engPasswordDataGridViewTextBoxColumn
            // 
            this.engPasswordDataGridViewTextBoxColumn.DataPropertyName = "engPassword";
            this.engPasswordDataGridViewTextBoxColumn.HeaderText = "engPassword";
            this.engPasswordDataGridViewTextBoxColumn.Name = "engPasswordDataGridViewTextBoxColumn";
            // 
            // crossPeriodDataGridViewTextBoxColumn
            // 
            this.crossPeriodDataGridViewTextBoxColumn.DataPropertyName = "crossPeriod";
            this.crossPeriodDataGridViewTextBoxColumn.HeaderText = "crossPeriod";
            this.crossPeriodDataGridViewTextBoxColumn.Name = "crossPeriodDataGridViewTextBoxColumn";
            // 
            // longPeriodDataGridViewTextBoxColumn
            // 
            this.longPeriodDataGridViewTextBoxColumn.DataPropertyName = "longPeriod";
            this.longPeriodDataGridViewTextBoxColumn.HeaderText = "longPeriod";
            this.longPeriodDataGridViewTextBoxColumn.Name = "longPeriodDataGridViewTextBoxColumn";
            // 
            // crossDampFactorDataGridViewTextBoxColumn
            // 
            this.crossDampFactorDataGridViewTextBoxColumn.DataPropertyName = "crossDampFactor";
            this.crossDampFactorDataGridViewTextBoxColumn.HeaderText = "crossDampFactor";
            this.crossDampFactorDataGridViewTextBoxColumn.Name = "crossDampFactorDataGridViewTextBoxColumn";
            // 
            // longDampFactorDataGridViewTextBoxColumn
            // 
            this.longDampFactorDataGridViewTextBoxColumn.DataPropertyName = "longDampFactor";
            this.longDampFactorDataGridViewTextBoxColumn.HeaderText = "longDampFactor";
            this.longDampFactorDataGridViewTextBoxColumn.Name = "longDampFactorDataGridViewTextBoxColumn";
            // 
            // crossGainDataGridViewTextBoxColumn
            // 
            this.crossGainDataGridViewTextBoxColumn.DataPropertyName = "crossGain";
            this.crossGainDataGridViewTextBoxColumn.HeaderText = "crossGain";
            this.crossGainDataGridViewTextBoxColumn.Name = "crossGainDataGridViewTextBoxColumn";
            // 
            // longGainDataGridViewTextBoxColumn
            // 
            this.longGainDataGridViewTextBoxColumn.DataPropertyName = "longGain";
            this.longGainDataGridViewTextBoxColumn.HeaderText = "longGain";
            this.longGainDataGridViewTextBoxColumn.Name = "longGainDataGridViewTextBoxColumn";
            // 
            // crossLeadDataGridViewTextBoxColumn
            // 
            this.crossLeadDataGridViewTextBoxColumn.DataPropertyName = "crossLead";
            this.crossLeadDataGridViewTextBoxColumn.HeaderText = "crossLead";
            this.crossLeadDataGridViewTextBoxColumn.Name = "crossLeadDataGridViewTextBoxColumn";
            // 
            // longLeadDataGridViewTextBoxColumn
            // 
            this.longLeadDataGridViewTextBoxColumn.DataPropertyName = "longLead";
            this.longLeadDataGridViewTextBoxColumn.HeaderText = "longLead";
            this.longLeadDataGridViewTextBoxColumn.Name = "longLeadDataGridViewTextBoxColumn";
            // 
            // springTensionMaxDataGridViewTextBoxColumn
            // 
            this.springTensionMaxDataGridViewTextBoxColumn.DataPropertyName = "springTensionMax";
            this.springTensionMaxDataGridViewTextBoxColumn.HeaderText = "springTensionMax";
            this.springTensionMaxDataGridViewTextBoxColumn.Name = "springTensionMaxDataGridViewTextBoxColumn";
            // 
            // crossBiasDataGridViewTextBoxColumn
            // 
            this.crossBiasDataGridViewTextBoxColumn.DataPropertyName = "crossBias";
            this.crossBiasDataGridViewTextBoxColumn.HeaderText = "crossBias";
            this.crossBiasDataGridViewTextBoxColumn.Name = "crossBiasDataGridViewTextBoxColumn";
            // 
            // longBiasDataGridViewTextBoxColumn
            // 
            this.longBiasDataGridViewTextBoxColumn.DataPropertyName = "longBias";
            this.longBiasDataGridViewTextBoxColumn.HeaderText = "longBias";
            this.longBiasDataGridViewTextBoxColumn.Name = "longBiasDataGridViewTextBoxColumn";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // configDataBindingSource
            // 
            this.configDataBindingSource.DataMember = "ConfigData";
            this.configDataBindingSource.DataSource = this.dynamicDataDataSet5;
            // 
            // dynamicDataDataSet5
            // 
            this.dynamicDataDataSet5.DataSetName = "DynamicDataDataSet5";
            this.dynamicDataDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // configDataTableAdapter
            // 
            this.configDataTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 460);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.Load += new System.EventHandler(this.DataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dynamicDataDataSet5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DynamicDataDataSet5 dynamicDataDataSet5;
        private System.Windows.Forms.BindingSource configDataBindingSource;
        private DynamicDataDataSet5TableAdapters.ConfigDataTableAdapter configDataTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn meterNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn beamScaleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn engPasswordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossPeriodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longPeriodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossDampFactorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longDampFactorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossGainDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longGainDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossLeadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longLeadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn springTensionMaxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crossBiasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longBiasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
    }
}