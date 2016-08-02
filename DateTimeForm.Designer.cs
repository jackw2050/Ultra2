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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.dayLabel = new System.Windows.Forms.Label();
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
            this.dayOfYearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayOfYearLabel.Location = new System.Drawing.Point(17, 100);
            this.dayOfYearLabel.Name = "dayOfYearLabel";
            this.dayOfYearLabel.Size = new System.Drawing.Size(37, 17);
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
            this.button1.Location = new System.Drawing.Point(302, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(192, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Set to current Local Date/ Time";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(192, 190);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(189, 20);
            this.textBox1.TabIndex = 17;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(192, 129);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "Set to current Universal Date/ Time";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayLabel.Location = new System.Drawing.Point(60, 100);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(0, 17);
            this.dayLabel.TabIndex = 19;
            // 
            // DateTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 261);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label dayLabel;
    }
}