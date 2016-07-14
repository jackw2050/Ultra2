namespace SerialPortTerminal
{
    partial class DateTimeForm
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
            this.setDateTimeSuccessLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dayOfYearLabel = new System.Windows.Forms.Label();
            this.setDateTimeButton = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setDateTimeSuccessLabel
            // 
            this.setDateTimeSuccessLabel.AutoSize = true;
            this.setDateTimeSuccessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setDateTimeSuccessLabel.Location = new System.Drawing.Point(17, 235);
            this.setDateTimeSuccessLabel.Name = "setDateTimeSuccessLabel";
            this.setDateTimeSuccessLabel.Size = new System.Drawing.Size(79, 20);
            this.setDateTimeSuccessLabel.TabIndex = 14;
            this.setDateTimeSuccessLabel.Text = "Success?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Select new date and time for  embedded procesor";
            // 
            // dayOfYearLabel
            // 
            this.dayOfYearLabel.AutoSize = true;
            this.dayOfYearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayOfYearLabel.Location = new System.Drawing.Point(17, 100);
            this.dayOfYearLabel.Name = "dayOfYearLabel";
            this.dayOfYearLabel.Size = new System.Drawing.Size(41, 20);
            this.dayOfYearLabel.TabIndex = 12;
            this.dayOfYearLabel.Text = "Day ";
            // 
            // setDateTimeButton
            // 
            this.setDateTimeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.setDateTimeButton.Location = new System.Drawing.Point(21, 177);
            this.setDateTimeButton.Name = "setDateTimeButton";
            this.setDateTimeButton.Size = new System.Drawing.Size(106, 23);
            this.setDateTimeButton.TabIndex = 11;
            this.setDateTimeButton.Text = "Set Date / Time";
            this.setDateTimeButton.UseVisualStyleBackColor = true;
            this.setDateTimeButton.Click += new System.EventHandler(this.setDateTimeButton_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(20, 52);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(292, 20);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(257, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DateTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.setDateTimeSuccessLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dayOfYearLabel);
            this.Controls.Add(this.setDateTimeButton);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "DateTimeForm";
            this.Text = "DateTimeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label setDateTimeSuccessLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dayOfYearLabel;
        private System.Windows.Forms.Button setDateTimeButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}