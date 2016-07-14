﻿namespace SerialPortTerminal
{
    partial class FileFormatForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateFormatRadioButton1 = new System.Windows.Forms.RadioButton();
            this.dateFormatRadioButton2 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.dateFormatRadioButton3 = new System.Windows.Forms.RadioButton();
            this.customFileNameRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textRadioButton = new System.Windows.Forms.RadioButton();
            this.commaRadioButton = new System.Windows.Forms.RadioButton();
            this.tabRadioButton = new System.Windows.Forms.RadioButton();
            this.sampleFileNamelabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.CustomNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateFormatRadioButton1);
            this.groupBox2.Controls.Add(this.dateFormatRadioButton2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dateFormatRadioButton3);
            this.groupBox2.Controls.Add(this.customFileNameRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(30, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 121);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select a date format";
            // 
            // dateFormatRadioButton1
            // 
            this.dateFormatRadioButton1.Location = new System.Drawing.Point(6, 19);
            this.dateFormatRadioButton1.Name = "dateFormatRadioButton1";
            this.dateFormatRadioButton1.Size = new System.Drawing.Size(139, 17);
            this.dateFormatRadioButton1.TabIndex = 0;
            this.dateFormatRadioButton1.TabStop = true;
            this.dateFormatRadioButton1.Text = "2015-Jan-1-15-23-34";
            this.dateFormatRadioButton1.UseVisualStyleBackColor = true;
            this.dateFormatRadioButton1.CheckedChanged += new System.EventHandler(this.dateFormatRadioButton1_CheckedChanged);
            // 
            // dateFormatRadioButton2
            // 
            this.dateFormatRadioButton2.Location = new System.Drawing.Point(6, 42);
            this.dateFormatRadioButton2.Name = "dateFormatRadioButton2";
            this.dateFormatRadioButton2.Size = new System.Drawing.Size(118, 17);
            this.dateFormatRadioButton2.TabIndex = 1;
            this.dateFormatRadioButton2.TabStop = true;
            this.dateFormatRadioButton2.Text = "2015-1-1-15-23-34";
            this.dateFormatRadioButton2.UseVisualStyleBackColor = true;
            this.dateFormatRadioButton2.CheckedChanged += new System.EventHandler(this.dateFormatRadioButton2_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Julian day";
            // 
            // dateFormatRadioButton3
            // 
            this.dateFormatRadioButton3.Location = new System.Drawing.Point(6, 66);
            this.dateFormatRadioButton3.Name = "dateFormatRadioButton3";
            this.dateFormatRadioButton3.Size = new System.Drawing.Size(128, 17);
            this.dateFormatRadioButton3.TabIndex = 2;
            this.dateFormatRadioButton3.TabStop = true;
            this.dateFormatRadioButton3.Text = "2015-1-15-23-34";
            this.dateFormatRadioButton3.UseVisualStyleBackColor = true;
            this.dateFormatRadioButton3.CheckedChanged += new System.EventHandler(this.dateFormatRadioButton3_CheckedChanged);
            // 
            // customFileNameRadioButton
            // 
            this.customFileNameRadioButton.AutoSize = true;
            this.customFileNameRadioButton.Location = new System.Drawing.Point(6, 89);
            this.customFileNameRadioButton.Name = "customFileNameRadioButton";
            this.customFileNameRadioButton.Size = new System.Drawing.Size(105, 17);
            this.customFileNameRadioButton.TabIndex = 10;
            this.customFileNameRadioButton.TabStop = true;
            this.customFileNameRadioButton.Text = "Custom file name";
            this.customFileNameRadioButton.UseVisualStyleBackColor = true;
            this.customFileNameRadioButton.CheckedChanged += new System.EventHandler(this.customFileNameRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textRadioButton);
            this.groupBox1.Controls.Add(this.commaRadioButton);
            this.groupBox1.Controls.Add(this.tabRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(294, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select file format";
            // 
            // textRadioButton
            // 
            this.textRadioButton.Location = new System.Drawing.Point(6, 19);
            this.textRadioButton.Name = "textRadioButton";
            this.textRadioButton.Size = new System.Drawing.Size(164, 17);
            this.textRadioButton.TabIndex = 3;
            this.textRadioButton.TabStop = true;
            this.textRadioButton.Text = "Text (*.txt)";
            this.textRadioButton.UseVisualStyleBackColor = true;
            this.textRadioButton.CheckedChanged += new System.EventHandler(this.textRadioButton_CheckedChanged);
            // 
            // commaRadioButton
            // 
            this.commaRadioButton.Location = new System.Drawing.Point(6, 42);
            this.commaRadioButton.Name = "commaRadioButton";
            this.commaRadioButton.Size = new System.Drawing.Size(164, 17);
            this.commaRadioButton.TabIndex = 4;
            this.commaRadioButton.TabStop = true;
            this.commaRadioButton.Text = "Comma delimited (*.csv)";
            this.commaRadioButton.UseVisualStyleBackColor = true;
            this.commaRadioButton.CheckedChanged += new System.EventHandler(this.commaRadioButton_CheckedChanged);
            // 
            // tabRadioButton
            // 
            this.tabRadioButton.Location = new System.Drawing.Point(6, 65);
            this.tabRadioButton.Name = "tabRadioButton";
            this.tabRadioButton.Size = new System.Drawing.Size(164, 17);
            this.tabRadioButton.TabIndex = 5;
            this.tabRadioButton.TabStop = true;
            this.tabRadioButton.Text = "Tab delimited (*.tsv)";
            this.tabRadioButton.UseVisualStyleBackColor = true;
            this.tabRadioButton.CheckedChanged += new System.EventHandler(this.tabRadioButton_CheckedChanged);
            // 
            // sampleFileNamelabel
            // 
            this.sampleFileNamelabel.AutoSize = true;
            this.sampleFileNamelabel.Location = new System.Drawing.Point(27, 353);
            this.sampleFileNamelabel.Name = "sampleFileNamelabel";
            this.sampleFileNamelabel.Size = new System.Drawing.Size(87, 13);
            this.sampleFileNamelabel.TabIndex = 22;
            this.sampleFileNamelabel.Text = "Sample file name";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(398, 306);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 21;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // CustomNameTextBox
            // 
            this.CustomNameTextBox.Location = new System.Drawing.Point(30, 240);
            this.CustomNameTextBox.Name = "CustomNameTextBox";
            this.CustomNameTextBox.Size = new System.Drawing.Size(405, 20);
            this.CustomNameTextBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Survey name + date / time . file type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "File names are created as follows:";
            // 
            // FileFormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 401);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sampleFileNamelabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.CustomNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FileFormatForm";
            this.Text = "FileFormatForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton dateFormatRadioButton1;
        private System.Windows.Forms.RadioButton dateFormatRadioButton2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton dateFormatRadioButton3;
        private System.Windows.Forms.RadioButton customFileNameRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton textRadioButton;
        private System.Windows.Forms.RadioButton commaRadioButton;
        private System.Windows.Forms.RadioButton tabRadioButton;
        private System.Windows.Forms.Label sampleFileNamelabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox CustomNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}