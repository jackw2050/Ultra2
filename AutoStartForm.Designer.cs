namespace SerialPortTerminal
{
    partial class AutoStartForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.crossGyroStatusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.heaterStatusLabel = new System.Windows.Forms.Label();
            this.longGyroStatusLabel = new System.Windows.Forms.Label();
            this.gyroStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonCancel = new System.Windows.Forms.RadioButton();
            this.radioButtonContinue = new System.Windows.Forms.RadioButton();
            this.radioButtonWait = new System.Windows.Forms.RadioButton();
            this.gyroStatusGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cross";
            // 
            // crossGyroStatusLabel
            // 
            this.crossGyroStatusLabel.AutoSize = true;
            this.crossGyroStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crossGyroStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.crossGyroStatusLabel.Location = new System.Drawing.Point(127, 50);
            this.crossGyroStatusLabel.Name = "crossGyroStatusLabel";
            this.crossGyroStatusLabel.Size = new System.Drawing.Size(82, 16);
            this.crossGyroStatusLabel.TabIndex = 4;
            this.crossGyroStatusLabel.Text = "Not Ready";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Long";
            // 
            // heaterStatusLabel
            // 
            this.heaterStatusLabel.AutoSize = true;
            this.heaterStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heaterStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.heaterStatusLabel.Location = new System.Drawing.Point(35, 50);
            this.heaterStatusLabel.Name = "heaterStatusLabel";
            this.heaterStatusLabel.Size = new System.Drawing.Size(82, 16);
            this.heaterStatusLabel.TabIndex = 6;
            this.heaterStatusLabel.Text = "Not Ready";
            // 
            // longGyroStatusLabel
            // 
            this.longGyroStatusLabel.AutoSize = true;
            this.longGyroStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longGyroStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.longGyroStatusLabel.Location = new System.Drawing.Point(127, 101);
            this.longGyroStatusLabel.Name = "longGyroStatusLabel";
            this.longGyroStatusLabel.Size = new System.Drawing.Size(82, 16);
            this.longGyroStatusLabel.TabIndex = 7;
            this.longGyroStatusLabel.Text = "Not Ready";
            // 
            // gyroStatusGroupBox
            // 
            this.gyroStatusGroupBox.Controls.Add(this.label4);
            this.gyroStatusGroupBox.Controls.Add(this.longGyroStatusLabel);
            this.gyroStatusGroupBox.Controls.Add(this.crossGyroStatusLabel);
            this.gyroStatusGroupBox.Controls.Add(this.label6);
            this.gyroStatusGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gyroStatusGroupBox.Location = new System.Drawing.Point(12, 28);
            this.gyroStatusGroupBox.Name = "gyroStatusGroupBox";
            this.gyroStatusGroupBox.Size = new System.Drawing.Size(240, 184);
            this.gyroStatusGroupBox.TabIndex = 8;
            this.gyroStatusGroupBox.TabStop = false;
            this.gyroStatusGroupBox.Text = "Gyro Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonCancel);
            this.groupBox2.Controls.Add(this.radioButtonContinue);
            this.groupBox2.Controls.Add(this.radioButtonWait);
            this.groupBox2.Controls.Add(this.heaterStatusLabel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(296, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 184);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Heater Status";
            // 
            // radioButtonCancel
            // 
            this.radioButtonCancel.AutoSize = true;
            this.radioButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCancel.Location = new System.Drawing.Point(24, 149);
            this.radioButtonCancel.Name = "radioButtonCancel";
            this.radioButtonCancel.Size = new System.Drawing.Size(128, 20);
            this.radioButtonCancel.TabIndex = 9;
            this.radioButtonCancel.Text = "Cancel Auto Start";
            this.radioButtonCancel.UseVisualStyleBackColor = true;
            this.radioButtonCancel.CheckedChanged += new System.EventHandler(this.radioButtonCancel_CheckedChanged);
            // 
            // radioButtonContinue
            // 
            this.radioButtonContinue.AutoSize = true;
            this.radioButtonContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonContinue.Location = new System.Drawing.Point(24, 123);
            this.radioButtonContinue.Name = "radioButtonContinue";
            this.radioButtonContinue.Size = new System.Drawing.Size(165, 20);
            this.radioButtonContinue.TabIndex = 8;
            this.radioButtonContinue.Text = "Continue without Heater";
            this.radioButtonContinue.UseVisualStyleBackColor = true;
            this.radioButtonContinue.CheckedChanged += new System.EventHandler(this.radioButtonContinue_CheckedChanged);
            // 
            // radioButtonWait
            // 
            this.radioButtonWait.AutoSize = true;
            this.radioButtonWait.Checked = true;
            this.radioButtonWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWait.Location = new System.Drawing.Point(24, 97);
            this.radioButtonWait.Name = "radioButtonWait";
            this.radioButtonWait.Size = new System.Drawing.Size(115, 20);
            this.radioButtonWait.TabIndex = 7;
            this.radioButtonWait.TabStop = true;
            this.radioButtonWait.Text = "Wait for Heater";
            this.radioButtonWait.UseVisualStyleBackColor = true;
            this.radioButtonWait.CheckedChanged += new System.EventHandler(this.radioButtonWait_CheckedChanged);
            // 
            // AutoStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 260);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gyroStatusGroupBox);
            this.Name = "AutoStartForm";
            this.Text = "Status";
            this.Load += new System.EventHandler(this.AutoStartForm_Load);
            this.gyroStatusGroupBox.ResumeLayout(false);
            this.gyroStatusGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gyroStatusGroupBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonCancel;
        private System.Windows.Forms.RadioButton radioButtonContinue;
        private System.Windows.Forms.RadioButton radioButtonWait;
        public System.Windows.Forms.Label crossGyroStatusLabel;
        public System.Windows.Forms.Label heaterStatusLabel;
        public System.Windows.Forms.Label longGyroStatusLabel;
    }
}